if (global.playerCarrying == noone) {
    global.playerCarrying = obj_Guy;
    MoveSpeed -= 3;
    with (other) {
        x = -1000;
        y = -1000;
    }
}