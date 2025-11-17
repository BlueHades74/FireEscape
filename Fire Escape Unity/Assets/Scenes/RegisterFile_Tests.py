import os

import cocotb

from cocotb.clock import Clock
from cocotb.runner import get_runner
from cocotb.triggers import RisingEdge

register_file_sim_dir = os.path.abspath(os.path.join('.', 'register_file_sim_dir'))


@cocotb.test()
async def run_register_file_sim(dut):
    clock = Clock(dut.clk, period=10, units='ns')  # This assigns the clock into the register file

    cocotb.start_soon(clock.start(start_high=False))

    for rdi in range(32):
        await RisingEdge(dut.clk)
        dut.rd.value = rdi
        dut.rd_data.value = rdi
        dut.read_enable.value = 0
        dut.write_enable.value = 1

    await RisingEdge(dut.clk)
    dut.write_enable.value = 0

    rs1_values = []
    rs2_values = []

    for rsi in range(32):
        await RisingEdge(dut.clk)
        dut.rs1.value = rsi
        dut.rs2.value = rsi
        dut.read_enable.value = 1
        rs1_values.append(int(dut.rs1_data.value))
        rs2_values.append(int(dut.rs2_data.value))

    await RisingEdge(dut.clk)
    dut.write_enable.value = 0
    dut.read_enable.value = 0
    # Reset the read register locations for next use
    dut.rs1.value = 0
    dut.rs2.value = 0
    rs1_values.append(int(dut.rs1_data.value))
    rs2_values.append(int(dut.rs2_data.value))

    await RisingEdge(dut.clk)
    dut.write_enable.value = 0
    dut.read_enable.value = 0

    await RisingEdge(dut.clk)
    dut.write_enable.value = 0
    dut.read_enable.value = 0
    rs1_values.append(int(dut.rs1_data.value))
    rs2_values.append(int(dut.rs2_data.value))

    for ii in range(32):  # Note the delay in the results of one clock cycle
        assert rs1_values[ii + 1] == ii
        assert rs2_values[ii + 1] == ii


def test_via_cocotb():
    """
    Main entry point for cocotb
    """
    verilog_sources = [os.path.abspath(os.path.join('.', 'register_file.v'))]
    runner = get_runner("verilator")
    runner.build(
        verilog_sources=verilog_sources,
        vhdl_sources=[],
        hdl_toplevel="RISC_REGISTER_FILE",
        build_args=["--threads", "2"],
        build_dir=register_file_sim_dir,
    )
    runner.test(hdl_toplevel="RISC_REGISTER_FILE", test_module="register_file_sim")

@cocotb.test()
async def btbLoad(dut):
    #simulate:
    #lw x7, 0(x10)
    #lw x8, 0(x11)
    dut.reset.value = 1
    await Timer(10, units ='ns')
    dut.reset.value = 0

    #first instruction
    #lw x7, 0(x10)
    dut.read_reg1.value = 10
    dut.write_reg.value = 7
    dut.write_data.value = 0xA0
    dut.reg_write.value = 1

    await RisingEdge(dut.clk)

    #keep result
    cocotb.log.info(f"Wrote 0x{dut.write_data.value.integer:X} to x{dut.write_reg.value.integer}")

    #2nd instruction
    #lw x8, 0(x11)
    dut.read_reg1.value = 11
    dut.write_reg.value = 8
    dut.write_data.value = 0xB0
    dut.reg_write.value = 1

    await RisingEdge(dut.clk)

    #keep
    cocotb.log.info(f"Wrote 0x{dut.write_data.value.integer:X} to x{dut.write_reg.value.integer}")

    #end write
    dut.reg_write.value = 0
    await RisingEdge(dut.clk)

    #read results
    dut.read_reg1.value = 7
    dut.read_reg2.value = 8  
    
    await Timer(1, units='ns')

    #check results:
    val7 = dut.read_data1.value.integer
    val8 = dut.read_data2.value.integer

    cocotb.log.info(f"Read x7 = 0x{val7:X}, x8 = 0x{val8:X}")
    assert val7 == 0xA0 and val8 == 0xB0, "Failed back to back load test :("

@cocotb.test()
async def btbALU(dut):
        #simulate:
    #add x5, x6, x7
    #add x6, x7, x8
    dut.reset.value = 1
    await Timer(10, units ='ns')
    dut.reset.value = 0

    #first instruction
    #add x5, x6, x7
    dut.read_reg1.value = 6
    dut.read_reg2.value = 7
    dut.write_reg.value = 7
    dut.write_data.value = 5
    dut.reg_write.value = 1

    await RisingEdge(dut.clk)

    #keep result
    cocotb.log.info(f"Wrote 0x{dut.write_data.value.integer:X} to x{dut.write_reg.value.integer}")

    #2nd instruction
    #add x6, x7, x8
    dut.read_reg1.value = 7
    dut.read_reg2.value = 8
    dut.write_reg.value = 8
    dut.write_data.value = 6
    dut.reg_write.value = 1

    await RisingEdge(dut.clk)

    #keep
    cocotb.log.info(f"Wrote 0x{dut.write_data.value.integer:X} to x{dut.write_reg.value.integer}")

    #end write
    dut.reg_write.value = 0
    await RisingEdge(dut.clk)

    #read results
    dut.read_reg1.value = 7
    dut.read_reg2.value = 8  
    
    await Timer(1, units='ns')

    #check results:
    val7 = dut.read_data1.value.integer
    val8 = dut.read_data2.value.integer

    cocotb.log.info(f"Read x7 = 0x{val7:X}, x8 = 0x{val8:X}")
    assert val7 == 5 and val8 == 6, "Failed back to back ALU test :("

@cocotb.test()
async def btbStore(dut):
    #do: 
    #sw x5, 0(x10)
    #sw x6, 0(x11)
    dut.reset.value = 1
    await Timer(10, units ='ns')
    dut.reset.value = 0

    #first instruction
    #sw x5, 0(x10)
    dut.read_reg1.value = 5
    dut.read_reg2.value = 10
    cocotb.log.info("stored value of x5 in adress x10")
    await RisingEdge(dut.clk)

    #2nd instr
    #sw x6, 0(x11)
    dut.read_reg1.value = 6
    dut.read_reg2.value = 11
    cocotb.log.info("stored value of x7 in adress x11")
    
    await RisingEdge(dut.clk)

    cocotb.log.info("Passed btb store test :)")

@cocotb.test()
async def loadALUstore(dut):
    #Do:
    #lw x7, 0(x10)
    #add x5, x6, x7
    #sw x5, 0(x10)

    #reset as we have been
    dut.reset.value = 1
    await Timer(10, units ='ns')
    dut.reset.value = 0

    #first
    #lw x7, 0(x10)
    dut.read_reg1.value = 10
    dut.write_reg.value = 7
    dut.write_data.value = 0xA0
    dut.reg_write.value = 1

    await RisingEdge(dut.clk)

    #2nd
    #add x5, x6, x7
    dut.read_reg1.value = 6
    dut.read_reg2.value = 7
    dut.write_reg.value = 7
    dut.write_data.value = 5
    dut.reg_write.value = 1

    await RisingEdge(dut.clk)

    #third 
    #sw x5, 0(x10)
    dut.read_reg1.value = 5
    dut.read_reg2.value = 10
    cocotb.log.info("stored value of x5 in adress x10")

    await RisingEdge(dut.clk)

    cocotb.log.info("finished load, alu, store sequence")

@cocotb.test()
async def memSwap(dut):
    #do:
    #lw x28, 0(x10)
    #lw x29, 0(x11)
    #sw x28, 0(x11)
    #sw x29, 0(x10)

    #reset as we have been
    dut.reset.value = 1
    await Timer(10, units ='ns')
    dut.reset.value = 0

    #first
    #lw x28, 0(x10)
    dut.read_reg1.value = 10
    dut.write_reg.value = 28
    dut.write_data.value = 0xA0
    dut.reg_write.value = 1

    await RisingEdge(dut.clk)
    
    #2nd
    #lw x29, 0(x11)
    dut.read_reg1.value = 11
    dut.write_reg.value = 29
    dut.write_data.value = 0xA0
    dut.reg_write.value = 1

    await RisingEdge(dut.clk)
    
    #3rd
    #sw x29, 0(x10)
    dut.read_reg1.value = 29
    dut.read_reg2.value = 10
    cocotb.log.info("stored value of x29 in adress x10")

    await RisingEdge(dut.clk)
    
    #4th
    #sw x28, 0(x10)
    dut.read_reg1.value = 28
    dut.read_reg2.value = 10
    cocotb.log.info("stored value of x29 in adress x10")
    
    await RisingEdge(dut.clk)

    cocotb.log.info("complete memory swap test :)")
    
if __name__ == '__main__':
    test_via_cocotb()
