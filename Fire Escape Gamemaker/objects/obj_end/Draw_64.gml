var _numItems = ds_list_size(global.deliveredItems);
for (var i = 0; i < _numItems; i++) {
    var _item = global.deliveredItems[| i];
    draw_text(10, 10 + (i * 20), "Saved: " + object_get_name(_item));
}

var _undelivered = ds_list_create(); // Create a temporary list

// Iterate through all collectibles
var _numCollectibles = ds_list_size(global.allCollectibles);
for (var i = 0; i < _numCollectibles; i++) {
    var _collectible = global.allCollectibles[| i];

    // Check if the collectible is NOT in the deliveredItems list
    if (ds_list_find_index(global.deliveredItems, _collectible) == -1) {
        ds_list_add(_undelivered, _collectible); // Add to the undelivered list
    }
}

// Display the undelivered items
var _numUndelivered = ds_list_size(_undelivered);
if (_numUndelivered > 0) {
    draw_set_halign(fa_center); // Center the text horizontally
    draw_set_valign(fa_middle); // Center text vertically.
    for (var j = 0; j < ds_list_size(_undelivered); j++)
    {
        var missing_item = _undelivered[| j];
        draw_text(room_width / 2, room_height / 2 + j * 30, "You did not save: " + object_get_name(missing_item));
    }

    draw_set_halign(fa_left); // Reset alignment
    draw_set_valign(fa_top);   //reset
} else {
    draw_set_halign(fa_center);
    draw_text(room_width / 2, room_height / 2, "You saved everyone! and everything....");
    draw_set_halign(fa_left);
}
ds_list_destroy(_undelivered); // Clean up the temporary list