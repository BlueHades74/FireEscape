if (global.playerCarrying == noone) {
    global.playerCarrying = obj_JewelBox;
    MoveSpeed -= 3;
    with (other) {
        x = -1000;
        y = -1000;
    }
}