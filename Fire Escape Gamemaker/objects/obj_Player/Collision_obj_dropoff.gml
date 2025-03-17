if (global.playerCarrying != noone) {
    
	MoveSpeed = 5;
	
    ds_list_add(global.deliveredItems, global.playerCarrying);

    with (global.playerCarrying) {
        instance_destroy();
    }
    global.playerCarrying = noone;
}