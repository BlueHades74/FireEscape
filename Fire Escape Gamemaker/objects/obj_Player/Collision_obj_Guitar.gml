if (global.playerCarrying == noone) {
    global.playerCarrying = obj_Guitar;
    MoveSpeed -= 2;
    with (other) {
        x = -1000;
        y = -1000;
    }
}