MoveSpeed = 5;
last_direction = 1;
layid = layer_get_id("Tiles_1")
mapid = layer_tilemap_get_id(layid)

global.playerCarrying = noone;
global.foodCarrying = false;
global.guitarCarrying = false;
global.jewelBoxCarrying = false;
global.laptopCarrying = false;
global.suitcaseCarrying = false;