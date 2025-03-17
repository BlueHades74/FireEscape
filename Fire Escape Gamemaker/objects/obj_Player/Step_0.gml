var right = keyboard_check(ord("D"));
var left = keyboard_check(ord("A"));
var up = keyboard_check(ord("W"));
var down = keyboard_check(ord("S"));

var xinput = right - left;
var yinput = down - up;

move_and_collide(xinput * MoveSpeed, yinput * MoveSpeed, mapid)

if (global.playerCarrying != noone) {
    if (xinput != 0 || yinput != 0) {
        sprite_index = FireFighterCarrying01;
        image_speed = .5;
        if (xinput > 0) {
            image_xscale = 1;
            last_direction = 1;
        } else if (xinput < 0) {
            image_xscale = -1;
            last_direction = -1;
        }
    } else {
        sprite_index = FireFighterCarrying01;
        image_index = 0;
        image_speed = 0;
        image_xscale = last_direction;
    }
} else {
    if (xinput != 0 || yinput != 0) {
        sprite_index = FireFighter01;
        image_speed = .5;
        if (xinput > 0) {
            image_xscale = 1;
            last_direction = 1;
        } else if (xinput < 0) {
            image_xscale = -1;
            last_direction = -1;
        }
    } else {
        sprite_index = FireFighter01;
        image_index = 0;
        image_speed = 0;
        image_xscale = last_direction;
    }
}

