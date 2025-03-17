draw_self();

if (global.playerCarrying != noone) {
    var carried_object = global.playerCarrying;
    var sprite_to_draw = carried_object.sprite_index;
    var image_to_draw = carried_object.image_index;
    draw_sprite(sprite_to_draw, image_to_draw, x, y - 16);
}