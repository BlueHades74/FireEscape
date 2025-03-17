if (!place_meeting(x + fireSpreadWidth, y, obj_Fire) && x < room_width) {
	instance_create_layer(x + fireSpreadWidth, y, "Instances", obj_Fire);
}

if (!place_meeting(x - fireSpreadWidth, y, obj_Fire) && x >= 0) {
	instance_create_layer(x - fireSpreadWidth, y, "Instances", obj_Fire);
}

if (!place_meeting(x, y - fireSpreadWidth, obj_Fire) && y >= 0) {
	instance_create_layer(x, y - fireSpreadWidth, "Instances", obj_Fire);
}

if (!place_meeting(x, y + fireSpreadWidth, obj_Fire) && y < room_height) {
	instance_create_layer(x, y + fireSpreadWidth, "Instances", obj_Fire);
}

alarm[0] = 120;