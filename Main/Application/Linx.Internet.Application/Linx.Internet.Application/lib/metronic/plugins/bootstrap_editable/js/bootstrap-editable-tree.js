/**
Tree input

@class tree
@extends abstractinput
@final
@example
<a href="#" id="username" data-type="tree" data-pk="1">awesome</a>
<script>
$(function(){
    $('#username').editable({
        url: '/post',
        title: 'Enter username'
    });
});
</script>
**/
(function ($) {
    "use strict";

    var opt = null; 

    var Tree = function (options) {
        this.init('tree', options, Tree.defaults);
        options.jstree = options.jstree || {};
        opt = options;
    };

    $.fn.editableutils.inherit(Tree, $.fn.editabletypes.abstractinput);

    $.extend(Tree.prototype, {

        str2value: function (str, separator) {
        },

        render: function () {
            //apply jstree extension
            this.$input.jstree(this.options.xtree).bind("select_node.jstree", opt.xtree.onSelectedNode)
        }
    })

    Tree.defaults = $.extend({}, $.fn.editabletypes.abstractinput.defaults, {
        /**
        @property tpl 
        @default <input type="tree">
        **/
        //tpl: '<input type="hidden">',
        tpl: '<a href="javascript:;"></a>',

        xtree: {
            "core": {
                "themes": {
                    "responsive": false
                },
                "data": [
             { "id": "ajson1", "parent": "#", "text": "No simples" },
             { "id": "ajson2", "parent": "#", "text": "No pai" },
             { "id": "ajson3", "parent": "ajson2", "text": "Filho 1" },
             { "id": "ajson4", "parent": "ajson2", "text": "Filho 2" },
                ],
            },
            "types": {
                "default": {
                    "icon": "fa fa-folder icon-state-warning icon-lg"
                },
                "file": {
                    "icon": "fa fa-file icon-state-warning icon-lg"
                }
            },
            "plugins": ["types"]
        },
        showbuttons: true,
        /**
        Placeholder attribute of input. Shown when input is empty.

        @property placeholder 
        @type string
        @default null
        **/
        placeholder: null,

        /**
        Whether to show `clear` button 
        
        @property clear 
        @type boolean
        @default true        
        **/
        clear: true
    });

    $.fn.editabletypes.tree = Tree;

}(window.jQuery));