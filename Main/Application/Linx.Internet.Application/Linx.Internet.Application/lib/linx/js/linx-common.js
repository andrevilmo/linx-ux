//#region Initializers
Globalize.culture('pt-BR');
kendo.culture("pt-BR");

$.support.cors = true;
$('div.ui-igpopover div.row:last-child:not(.predefValue) input').live('focusout', function (e) {
    if ($(e.relatedTarget).parents('.ui-igpopover').length == 0 && e.target.tagName == 'INPUT' && !e.target.hasClassName('hasDatepicker')) {
        $('button.filterRange.open').igPopover('hide');
        e.preventDefault();
    }
});

$('html:not(.ui-igpopover)').on('click', function (e) {
    if ($('button.filterRange.open').length > 0 && !$(e.target).parents().is('.ui-igpopover') && !$(e.target).parents().is('.ui-datepicker-header') && !$(e.target).is('.ui-igcombo-listitem')) {
        $('button.filterRange.open').igPopover('hide');
    }
});

//#endregion 
//#region Constants
var regexGUID = /^[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}$/i;
//#endregion Constants
//#region TabControl
function initializeTabControl(selector) {

    if ($(selector).length === 0) return;

    var hidWidth;
    var scrollBarWidths = 40;

    var widthOfList = function (selector) {
        var itemsWidth = 0;
        $(selector + '_list > li').each(function () {
            var itemWidth = $(this).outerWidth();
            itemsWidth += itemWidth;
        });
        return itemsWidth;
    };

    var widthOfHidden = function (selector) {
        return (($(selector + '_wrapper').outerWidth()) - widthOfList(selector) - getLeftPosi(selector)) - scrollBarWidths;
    };

    var getLeftPosi = function (selector) {
        if ($(selector + '_list').length == 0)
            return 0;

        return $(selector + '_list').position().left;
    };

    var reAdjust = function (selector) {
        var showScrollRight = (($(selector + '_wrapper').outerWidth()) < widthOfList(selector));
        if (showScrollRight) {
            $(selector + '_scroller_right').show();
        }
        else {
            $(selector + '_scroller_right').hide();
        }

        if (getLeftPosi() < 0) {
            $(selector + '_scroller_left').show();
        }
        else {
            if (showScrollRight)
                $(selector + '_list').animate({ left: "-=" + getLeftPosi() + "px" }, 'slow');
            else
                $(selector + '_list').animate({ left: "0px" }, 'slow');

            $(selector + '_scroller_left').hide();
        }
    }

    $(window).on('resize', function (e) {
        if (!e.isTrigger) { //WA pois estava ajustando também após selecionar uma tab
            reAdjust(selector);
        }
    });

    reAdjust(selector);

    $(selector + '_scroller_right').click(function () {
        $(this).css('pointer-events', 'none');
        setTimeout(function () {
            $(".scroller-right").css('pointer-events', '');
        }, 3 * 1000);
        $(selector + '_scroller_left').fadeIn('slow');
        $(selector + '_scroller_right').fadeOut('slow');

        $(selector + '_list').animate({ left: "+=" + widthOfHidden(selector) + "px" }, 'slow', function () { });
    });

    $(selector + '_scroller_left').click(function () {
        $(this).css('pointer-events', 'none');
        setTimeout(function () {
            $(".scroller-left").css('pointer-events', '');
        }, 3 * 1000);
        $(selector + '_scroller_right').fadeIn('slow');
        $(selector + '_scroller_left').fadeOut('slow');

        $(selector + '_list').animate({ left: "-=" + getLeftPosi(selector) + "px" }, 'slow', function () { });
    });
}
//#endregion TabControl

//#region Extension methods
String.prototype.mask = function (mask) {
    var m, l = (m = mask.split("")).length, s = this.split(""), j = 0, h = "", isUpper = false, isLower = false;
    var treat = function (s) {
        try {
            if (isUpper) s = s.toString().toUpperCase();
            if (isLower) s = s.toString().toLowerCase();
        } catch (e) { }
        return s;
    };
    for (var i = -1; ++i < l;) {
        if (m[i] === ">" || m[i] === "<") {
            if (m[i] === ">") isUpper = !isUpper;
            if (m[i] === "<") isLower = !isLower;
        } else {
            if (m[i] != "#") {
                if (m[i] == "\\" && (h += m[++i])) continue;
                h += m[i];
                i + 1 == l && (treat(s[j - 1]) += h, h = "");
            }
            else {
                if (!s[j] && !(h = "")) break;
                (s[j] = h + treat(s[j++])) && (h = "");
            }
        }
    }
    var formatted = s.join("") + treat(h);
    return isNullOrEmpty(formatted) ? '&nbsp;' : formatted;
};

String.prototype.replaceAll = function (from, to) {
    var str = this;
    var pos = str.indexOf(from);
    while (pos > -1) {
        str = str.replace(from, to);
        pos = str.indexOf(from);
    }
    return str.toString();
}

String.prototype.in = function (arrayItems) {
    return $.inArray(this.toString(), arrayItems) > -1;
};

Number.prototype.toLowerCase = function (numberValue) {
    return isNullOrEmpty(numberValue) ? '' : numberValue.toString();
};

Number.prototype.padLeft = function (totalWidth, paddingChar) {
    var stringReturn = '';
    if (isNullOrEmpty(paddingChar)) paddingChar = ' '; else paddingChar = paddingChar[0];

    for (var i = 0; i < totalWidth; i++)
        stringReturn += paddingChar;
    var text = this.toString().trim();
    return (stringReturn + text).substr(text.length, totalWidth);
};

function isNullOrEmpty(value) {
    return ((typeof value) === 'undefined') || value === null || value.toString().replaceAll(' ', '') === '' || value === '00000000-0000-0000-0000-000000000000' || value === 0 || (value instanceof Date && value.getFullYear() <= 1900) || (typeof value === 'boolean' && !value);
}

function isNull(value) {
    return ((typeof value) === 'undefined') || value === null;
}

function convertToString(value) {
    return (((typeof value) === 'undefined') || value === null ? '' : value.toString());
}

function convertNumberToText(valueToConvert) {
    if (typeof (valueToConvert) !== 'string' && typeof (valueToConvert) !== 'number') {
        return valueToConvert;
    }

    var n = typeof (valueToConvert) === 'string' ? valueToConvert : valueToConvert.toString()
    if (n.indexOf("e+") < 0) return n;
    var parts = n.split("e+");
    var first = parts[0].replace('.', "");
    var zeroes = parseInt(parts[1], 10) - (first.length - 1);
    for (var i = 0; i < zeroes; i++) {
        first += "0";
    }

    return first
}

function isEmptyEntityFn(entity) {
    return !isNull(entity) && typeof entity['isEmptyEntity'] != 'undefined' && entity['isEmptyEntity'];
}
//#endregion Extension methods

//#region Pivot Aggregators
var sumPropertyAggregator = function (items, propertyName) {
    var sum = 0;
    $.each(items, function (index, item) {
        sum += item[propertyName];
    });
    return sum;
};
var sumPropertyAggregatorFormat = function (val, dataType) {
    return $.ig.formatter(val, 'number', dataType, true, true, null);
};
//#endregion Pivot Aggregators

//#region MessageBox
var Buttons = {
    Ok: 'Ok',
    OkCancel: 'OkCancel',
    YesNo: 'YesNo',
    YesNoCancel: 'YesNoCancel'
};

var ButtonResult = {
    Ok: 1,
    Cancel: 2,
    Yes: 3,
    No: 4
};

var ImageClass = {
    'info': 'ui-icon ui-icon-info',
    'alert': 'ui-icon ui-icon-alert',
    'gear': 'ui-icon ui-icon-gear',
    'wrench': 'ui-icon ui-icon-wrench'
    //'home': 'ui-icon ui-icon-home'
    //'star': 'ui-icon ui-icon-star'
    //'heart': 'ui-icon ui-icon-heart'
    //'scissors': 'ui-icon ui-icon-scissors'
    //'person': 'ui-icon ui-icon-person'
    //'clock': 'ui-icon ui-icon-clock'
    //'trash': 'ui-icon ui-icon-trash'
    //'print': 'ui-icon ui-icon-print'
    //'flag': 'ui-icon ui-icon-flag'
};

function MessageInformation() {
    this.header = 'Information';
    this.message = 'message';
    this.adicionalInformation = '';
    this.buttons = Buttons.OkCancel;
    this.defaultButton = ButtonResult.Ok;
    this.imageClass = undefined;
    this.functionReturn = function (buttonResult) {

    };
}
MessageInformation.prototype.setButtons = function (newButton) {
    if (typeof newButton === 'string') {
        this.buttons = newButton;
    } else { alert('Please enter a valid Prop:buttons, type:Buttons.'); }
};
MessageInformation.prototype.setDefaultButton = function (newDefaultButton) {
    if (typeof newDefaultButton === 'number') {
        this.defaultButton = newDefaultButton;
    } else { alert('Please enter a valid Prop:defaultButton, type:ButtonResult.'); }
};
MessageInformation.prototype.setFunctionReturn = function (newFunctionReturn) {
    if (typeof newFunctionReturn === 'function') {
        this.functionReturn = newFunctionReturn;
    } else { alert('Please enter a valid functionReturn(function(ButtonResult buttonResult)).'); }
};
MessageInformation.prototype.show = function () {
    var self = this;
    messageShow(self);
};

function showWaitWindow(message, percent) {
    var $waitWindow = $("#waitWindowBox");

    if ($waitWindow.length == 0) {
        var $ctrl = $('<div class="modal js-loading-bar">' +
            '<div class="modal-dialog">' +
            ' <div class="modal-content">' +
            '    <div class="modal-body">' +
            '      <h4 class="alert-heading"></h4>' +
            '      <div class="progress progress-striped active">' +
            '          <div style="width: 0%;" class="progress-bar"></div>' +
            '      </div>' +
            '    </div>' +
            '  </div>' +
            '</div>' +
            '</div>').attr({ id: 'waitWindowBox' });

        $(this).parent().append($ctrl);
        $waitWindow = $ctrl;
    }

    $waitWindow.find('.alert-heading').text(message);
    $waitWindow.modal('show').css(
        {
            'margin-top': '20%'
        });
    $bar = $waitWindow.find('.progress-bar');
    if ($bar.length > 0) {
        $bar.addClass('animate');
        if (typeof percent != 'number')
            percent = 100
        if (percent >= 0 && percent <= 100) {
            $bar.width(percent.toString() + '%');
        }
    }
}

function closeWaitWindow() {
    var $waitWindow = $("#waitWindowBox");
    if ($waitWindow.length > 0) {
        $waitWindow.find('.alert-heading').text('');
        $bar = $waitWindow.find('.progress-bar.animate');
        if ($bar.length > 0) {
            $bar.removeClass('animate');
            $bar.width('0%');
        }
        $waitWindow.modal('hide');
    }
}

function messageShow(messageInformation) {
    /// <summary>Show the message in a alert for a user.</summary>
    /// <param name="messageInformation" type="MessageInformation">The message.</param>
    /// <returns type="Number">Return the div message.</returns>
    if (!(messageInformation instanceof MessageInformation)) {
        messageBoxException(new Error('The messageInformation dont type "MessageInformation".'));
        return null;
    }
    var msg = messageInformation, divExpanderInfo = null;
    //var msg = Object.create(MessageInformation, messageInformation);
    if ($("#msgbox") !== null) {
        $("#msgbox").remove();
    }

    var $ctrl = $('<div/>').attr({ id: 'msgbox', style: ' padding:0px;' }).height(200);
    var $divmsgText = $('<div/>').attr({ 'id': 'msgboxMessageText', style: 'padding: 15px 15px 15px 15px;' }).addClass("modal-body");
    var $pmsgText = $('<p/>').attr({ id: 'msgboxParagraphMessageText' });

    if (msg.adicionalInformation !== '') {
        divExpanderInfo = $("<div />").attr({ id: "msgboxExpanderInformationText" }).addClass('accordion');
        var pInfo = $("<p />").css({ 'overflow': 'auto' }).height(150).append(msg.adicionalInformation);

        divExpanderInfo.append(pInfo);
    }

    var $divmsgButtons = $('<div/>').attr({ id: 'msgboxMessageButtons' }).addClass('modal-footer');

    var btnClick = function (buttonResult) { msg.functionReturn(buttonResult); $ctrl.igDialog('close'); };

    var btnOk = $('<button/>').attr({ id: 'msgboxOkButton' }).append('Ok').click(function () { btnClick(ButtonResult.Ok); });
    var btnCancel = $('<button/>').attr({ id: 'msgboxCancelButton' }).append('Cancelar').click(function () { btnClick(ButtonResult.Cancel); });
    var btnYes = $('<button/>').attr({ id: 'msgboxYesButton' }).append('Sim').click(function () { btnClick(ButtonResult.Yes); });
    var btnNo = $('<button/>').attr({ id: 'msgboxNoButton' }).append('Não').click(function () { btnClick(ButtonResult.No); });
    btnOk.addClass('btn'); btnCancel.addClass('btn'); btnYes.addClass('btn'); btnNo.addClass('btn');
    if (msg.defaultButton !== null) {
        if (msg.defaultButton === ButtonResult.Ok) {
            btnOk.addClass('btn-primary');
        }
        if (msg.defaultButton === ButtonResult.Cancel) {
            btnCancel.addClass('btn-primary');
        }
        if (msg.defaultButton === ButtonResult.Yes) {
            btnYes.addClass('btn-primary');
        }
        if (msg.defaultButton === ButtonResult.No) {
            btnNo.addClass('btn-primary');
        }
    }

    switch (msg.buttons) {
        case Buttons.Ok:
            $divmsgButtons.append(btnOk);
            break;

        case Buttons.OkCancel:
            $divmsgButtons.append(btnOk);
            $divmsgButtons.append(btnCancel);
            break;

        case Buttons.YesNo:
            $divmsgButtons.append(btnYes);
            $divmsgButtons.append(btnNo);
            break;

        case Buttons.YesNoCancel:
            $divmsgButtons.append(btnYes);
            $divmsgButtons.append(btnNo);
            $divmsgButtons.append(btnCancel);
            break;

        default:
            throw msg.buttons + ' not exists.';
    }

    $divmsgText.append($pmsgText);
    $pmsgText.append(msg.message);
    $ctrl.append($divmsgText);
    if (typeof divExpanderInfo !== undefined) {
        $ctrl.append(divExpanderInfo);
    }
    $ctrl.append($divmsgButtons);

    $("body").append($ctrl);

    $ctrl.igDialog({
        headerText: msg.header,
        width: '80%',
        height: $(window).height() * 0.8,
        imageClass: msg.imageClass,
        modal: true,
        draggable: true,
        resizable: true,
        zIndex: getNew_zIndex()
    });



    return $ctrl;
}

function messageBoxException(exception) {
    /// <summary>Shows a message for a exception with details.</summary>
    /// <param name="exception" type="Error">The exception that occurred.</param>
    if (exception instanceof Error) {
        var msgInfo = new MessageInformation();
        msgInfo.header = 'Error';
        msgInfo.message = exception.message;
        msgInfo.setButtons(Buttons.Ok);
        msgInfo.setDefaultButton(ButtonResult.Ok);
        msgInfo.imageClass = ImageClass.alert;
        if (exception.stack !== null) {
            msgInfo.adicionalInformation = 'Details: ' + exception.stack;
        }
        msgInfo.show();
    }
    else {
        alert('The type "' + $exception.constructor.name + '" not expected!');
    }
}
//#endregion MessageBox

//#region Validations
function translateError(error, local) {
    if (local && error.indexOf("is required") != -1)
        return 'Informação requerida';
    else return error.toString().replace('is required', 'é requerido');
}

function toHTML(data) {
    data = data.replace(/\\r\\n/g, "<br />");
    data = data.replace(/\n/g, "<br />");
    return data;
}

function translateData(data) {
    var result = data;
    switch (data) {
        case 'Yes':
            result = 'Sim';
            break;
        case 'No':
            result = 'Não';
            break;
        case 'Cancel':
            result = 'Cancelar';
            break;
    }
    return result;
}

function strExtract(str, start, end) {
    var startIndex = str.indexOf(start);
    if (startIndex < 0 || startIndex >= str.length)
        return '';
    startIndex += start.length;
    var length = str.substring(startIndex, str.length).indexOf(end);
    if (length <= 0)
        return '';
    return str.substr(startIndex, length);
}

function strLeft(str, n) {
    if (n <= 0)
        return '';
    else if (n > str.length)
        return str
    else
        return str.substring(0, n);
}

function strRight(str, n) {
    if (n <= 0)
        return '';
    else if (n > str.length)
        return str;
    else
        var iLen = str.length;
    return str.substring(iLen - n, iLen);
}

function whereInArray(arrayList, predicate) {
    var items = arrayList.slice(0);
    var selectedItems = [];
    for (i = 0; i < items.length; i++) {
        if (predicate(items[i]))
            selectedItems.push(items[i]);
    }
    return selectedItems;
}


function addValidationError(element, message) {
    var formGroup = $(element).closest('.form-group');
    if (formGroup) {
        formGroup.addClass('has-error');
        var control = formGroup.find('.controls');
        if (control) {
            control.find('.linx-validation-error').each(function (i, item) { item.remove(); })
            var helpText = $('<i class="fa fa-warning linx-validation-error" title="' + translateError(message, true) + '"></i>');
            control.append(helpText);
        }
    }
}

function removeValidationError(element) {
    var formGroup = $(element).closest('.form-group');
    if (formGroup) {
        formGroup.removeClass('has-error');
        var control = formGroup.find('.controls');
        if (control) {
            control.find('.linx-validation-error').each(function (i, item) { item.remove(); })
        }
    }
}

ko.bindingHandlers.validatedField = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var entity = bindingContext.$data;
        var propertyName = ko.unwrap(valueAccessor());
        if (entity.entityAspect) {
            var vm = bindingContext.$root;

            var validationErrors = ko.observableArray([]);
            entity.entityAspect.validationErrorsChanged.subscribe(function (changes) {
                var newPropertyErrors = ko.utils.arrayFilter(changes.added, function (e) { return e.propertyName == propertyName });
                var removedPropertyErrors = ko.utils.arrayFilter(changes.removed, function (e) { return e.propertyName == propertyName });
                ko.utils.arrayForEach(newPropertyErrors, function (e) { validationErrors([]); validationErrors.push(e); });
                ko.utils.arrayForEach(removedPropertyErrors, function (e) { validationErrors.remove(e); });
            });
            validationErrors.subscribe(function (changes) {
                if (changes.length > 0 && vm.status() === 'E') {
                    addValidationError(element, changes[0].errorMessage)
                }
                else {
                    removeValidationError(element);
                }
            });
        }
        else if (entity.isPOCO) {
            var verify = function () {
                if (entity.ChangeState && entity.ChangeState.in(['I', 'U'])) {
                    var errors = entity.getValidationErrors(propertyName);
                    if (errors.length > 0) {
                        addValidationError(element, errors[0]);
                    }
                    else {
                        removeValidationError(element);
                    }
                }
            };
            verify();
            $(element).focusout(function () { setTimeout(function () { verify(); }, 300); });
        }
    }
};

function hasValueValidationFn(value, context) {
    return !isNullOrEmpty(value);
};
//#endregion

//#region selectGridCurrentItem
function selectGridCurrentItem(selectItemAction, primaryKey, ui, currentElement, viewSource) {
    if (!ui.manual) {
        var pkValue = 0;
        if (ui.hasOwnProperty('owner')) {
            if (ui.owner.grid.selectedRow() && ui.owner.grid.selectedRow().id)
                pkValue = ui.owner.grid.selectedRow().id;
            if (ui.owner.grid.selectedRows() && ui.owner.grid.selectedRows().length === 1)
                pkValue = ui.owner.grid.selectedRows()[0].id;
        } else {
            pkValue = ui.id;
        }
        if (selectItemAction && primaryKey && pkValue >= 0) {
            selectItemAction(primaryKey, pkValue, currentElement, viewSource);
        }
    }
}

function selectLightGridCurrentItem(selectItemAction, primaryKey, ui, currentElement, viewSource) {
    setTimeout(function () {
        if (!ui.manual) {
            var pkValue = 0;
            if (ui.owner) {
                if (ui.owner.grid.selectedRow() && ui.owner.grid.selectedRow().id)
                    pkValue = ui.owner.grid.selectedRow().id;
                if (ui.owner.grid.selectedRows())
                    pkValue = ui.owner.grid.selectedRows()[0].id;
            } else {
                pkValue = ui.id;
            }
            if (selectItemAction && primaryKey && pkValue >= 0) {
                selectItemAction(primaryKey, pkValue, currentElement, viewSource);
            }
        }
    }, 10);
}

function getGridDataSource(ui) {
    return ui.owner.grid.dataSource.dataSource();
}

function getGridVirtualItem(ui, index) {
    return ui.owner.grid.dataSource.dataView()[index];
}

//#endregion selectGridCurrentItem

//#region JExpression
function getJEntityExpression(reference, app, listFilterRange, removedFields, likeAsDefault, zeroFields) {
    /// <summary>Create a JExpression with the filled properties of a entity</summary>
    /// <param name="reference" type="object">entity.</param>
    /// <returns type="string"/>
    var jExpression = '';
    if (typeof removedFields === "undefined") {
        removedFields = [];
    }

    if ((typeof reference === 'undefined') || reference === null || (typeof reference.myProperties === 'undefined') || reference.myProperties === null) {
        return jExpression;
    }

    for (var idx in reference.myProperties) {
        //Property name
        var key = reference.myProperties[idx];
        if (removedFields.indexOf(key) >= 0)
            continue;
        //Property Value
        var value = reference[key]();

        var nameCol = reference.typeName + key;
        var begin = null, end = null, predefFilter = null, predefValue = null, hasValuePref = null;
        hasValuePref = typeof listFilterRange['has_' + nameCol] == 'function' ? listFilterRange['has_' + nameCol]() : null;

        if (typeof listFilterRange[nameCol + '_typeRange'] == 'function') {
            if (listFilterRange[nameCol + '_typeRange']() === 'R') {// filter by range
                begin = typeof listFilterRange[nameCol + '_begin'] == 'function' ? listFilterRange[nameCol + '_begin']() : null;
                end = typeof listFilterRange[nameCol + '_end'] == 'function' ? listFilterRange[nameCol + '_end']() : null;
            } else { // filter by predefineds
                predefFilter = (typeof listFilterRange[nameCol + '_predefFilter'] == 'function' && listFilterRange[nameCol + '_predefFilter']().length > 0) ? listFilterRange[nameCol + '_predefFilter']()[0] : null;
                predefValue = typeof listFilterRange[nameCol + '_predefValue'] == 'function' ? listFilterRange[nameCol + '_predefValue']() : null;
            }
        }
        else {
            begin = typeof listFilterRange[nameCol + '_begin'] == 'function' ? listFilterRange[nameCol + '_begin']() : null;
            end = typeof listFilterRange[nameCol + '_end'] == 'function' ? listFilterRange[nameCol + '_end']() : null;
        }
        var inList = typeof listFilterRange[nameCol] == 'function' ? listFilterRange[nameCol]() : null;

        if ((isNullOrEmpty(value) && isNullOrEmpty(hasValuePref) && isNullOrEmpty(begin) && isNullOrEmpty(end)) && !isNullOrEmpty(reference.queryRequiredProperties[key])) {
            app.showMessage('O filtro [' + reference.queryRequiredProperties[key] + '] é requerido na pesquisa, informe-o e tente novamente.', 'Alerta', ['Ok']);
            return 'Error';
        }

        var exp = getJEntityExpressionPart(reference, key, value, begin, end, inList, null, predefFilter, predefValue, likeAsDefault, zeroFields, hasValuePref);
        if (!isNullOrEmpty(exp) && exp.indexOf('error:') >= 0) {
            app.showMessage(exp.substr(exp.indexOf('error:') + 6), 'Alerta', ['Ok']);
            return 'Error';
        }
        if (!isNullOrEmpty(exp))
            jExpression += (jExpression === '' ? '' : ';') + exp;
    }

    return reference.typeName + "{" + jExpression + "}";
}

function getLookUpJEntityExpression(lookUpName, reference, key, value, extraFilters, referenceKey, app, likeAsDefault) {
    /// <summary>Create a JExpression for LookUps</summary>
    /// <param name="reference" type="object">entity.</param>
    /// <returns type="string"/>
    var jExpression = '';

    if ((typeof reference === 'undefined') || reference === null || isNullOrEmpty(lookUpName)) {
        return jExpression;
    }

    if (!isNullOrEmpty(key) && !isNullOrEmpty(value)) {
        var exp = getJEntityExpressionPart(reference, key, value, null, null, null, referenceKey, null, null, likeAsDefault);

        if (!isNullOrEmpty(exp) && exp.indexOf('error:') >= 0) {
            if (app)
                app.showMessage(exp.substr(exp.indexOf('error:') + 6), 'Alerta', ['Ok']);
            return 'Error';
        }

        if (!isNullOrEmpty(exp))
            jExpression += (jExpression === '' ? '' : ';') + exp;

    }
    if (!isNullOrEmpty(extraFilters)) {
        jExpression += (jExpression === '' ? '' : ';') + extraFilters;
    }

    return lookUpName + "{" + jExpression + "}";
}

function getJEntityExpressionPart(reference, key, value, begin, end, inList, referenceKey, predefFilter, predefValue, likeAsDefault, zeroFields, hasValuePref) {
    var jExpression = '';

    if ((typeof reference === 'undefined') || reference === null || isNullOrEmpty(key)) {
        return jExpression;
    }

    if (isNullOrEmpty(referenceKey))
        referenceKey = key;

    if (isNullOrEmpty(value))
        value = reference[referenceKey]();

    var dataType = reference.serverDataType[referenceKey];

    if (!isNullOrEmpty(inList) || dataType !== 'G' && ((isNullOrEmpty(value) ? '' : value.toString().trim()).indexOf('[') >= 0 || (isNullOrEmpty(value) ? '' : value.toString().trim()).indexOf(']') >= 2)) {
        if ((isNullOrEmpty(value) ? '' : value.toString().trim()).indexOf('[') >= 0 || (isNullOrEmpty(value) ? '' : value.toString().trim()).indexOf(']') >= 0)
            inList = isNullOrEmpty(value) ? '' : value.toString().trim();
        //verify the multi selection query is correct
        if ((inList.indexOf('[') >= 0 && inList.indexOf(']') < 0) || inList.indexOf('[') < 0 && inList.indexOf(']') >= 0) {
            return 'error:O campo ' + reference.getDisplayName(key) + ' está com um dos colchetes([,]) faltando.';
        }
        if (inList.indexOf('[') != inList.lastIndexOf('[')) {
            return 'error:O campo ' + reference.getDisplayName(key) + ' está com um dos colchetes([) a mais.';
        }
        if (inList.indexOf(']') != inList.lastIndexOf(']')) {
            return 'error:O campo ' + reference.getDisplayName(key) + ' está com um dos colchetes(]) a mais.';
        }

        var arrayIn = [];
        var charSurround = 'SCG'.indexOf(dataType) >= 0 ? '\'' : '';
        $.each(inList.toString().toLowerCase().substring(inList.indexOf('[') + 1, inList.lastIndexOf(']')).split(','), function (index, item) {
            arrayIn.push(charSurround + item.trim() + charSurround);
        });

        jExpression += (jExpression === '' ? '' : ';') + key + '#In#S' + encode(arrayIn.join(','));
    }

    var allowsZero = (zeroFields && value === 0 && zeroFields.indexOf && zeroFields.indexOf(key) >= 0);
    if ((dataType === 'B' || !isNullOrEmpty(value) || allowsZero) && isNullOrEmpty(inList)) {
        if (dataType === 'B') { //if boolean
            if (value != null) {
                jExpression += (jExpression === '' ? '' : ';') + key + '#==#' + dataType + encode(value.toString().toLowerCase());
            }
        }
        else if ('SCG'.indexOf(dataType) >= 0 && value !== '' && value !== '00000000-0000-0000-0000-000000000000') { //if string
            if (dataType === 'G' && !regexGUID.test(value)) return 'error:Valor não permitido para um campo Guid[' + reference.getDisplayName(key) + '].\nExemplo: ABCDEF01-2345-6789-ABCD-EF0123456789';
            if (likeAsDefault === true && dataType === 'S' && value.indexOf('%') === -1) {
                value = '%' + value + '%';
            }
            var operator = (value.indexOf('%') !== -1 ? 'Like' : '==');
            jExpression += (jExpression === '' ? '' : ';') + key + '#' + operator + '#' + dataType + encode(value);
        } else if ('LHIYDF'.indexOf(dataType) >= 0 && !isNaN(value.toString()) && (allowsZero || parseFloat(value.toString()) != 0)) { //if number
            jExpression += (jExpression === '' ? '' : ';') + key + '#==#' + dataType + encode(convertNumberToText(value));
        } else if (value instanceof Date && value.getFullYear() > 1900 && !hasValuePref) { //if DateTime
            if (value.getTimezoneOffset() > getUTCDate(value).getTimezoneOffset() && value.getUTCHours() === 0) {
                begin = end = getUTCDate(value);
            } else {

                var valueSearch = PrepareDateToSearch(value);
                if (valueSearch.initialDate != valueSearch.finalDate) {
                    jExpression += (jExpression === '' ? '' : ';') + key + '#>=#' + dataType + formatUTCDateToString(valueSearch.initialDate);
                    jExpression += (jExpression === '' ? '' : ';') + key + '#<=#' + dataType + formatUTCDateToString(valueSearch.finalDate);
                } else {
                    jExpression += (jExpression === '' ? '' : ';') + key + '#==#' + dataType + formatUTCDateToString(valueSearch.initialDate);
                }
            }
        }
    }

    if (!isNullOrEmpty(begin)) {
        if ('LHIYDF'.indexOf(dataType) >= 0 && parseFloat(begin.toString()) != 0) { //if number
            jExpression += (jExpression === '' ? '' : ';') + key + '#>=#' + dataType + encode(convertNumberToText(begin));
        } else if (begin instanceof Date && begin.getFullYear() > 1900) { //if DateTime
            jExpression += (jExpression === '' ? '' : ';') + key + '#>=#' + dataType + encode(begin.getUTCFullYear().toString() + '-' + (begin.getUTCMonth() + 1).toString() + '-' + begin.getUTCDate().toString() + ' 00:00:00.000');
        }
    }

    if (!isNullOrEmpty(end)) {
        if ('LHIYDF'.indexOf(dataType) >= 0 && parseFloat(end.toString()) != 0) { //if number
            jExpression += (jExpression === '' ? '' : ';') + key + '#<=#' + dataType + encode(convertNumberToText(end));
        } else if (end instanceof Date && end.getFullYear() > 1900) { //if DateTime
            jExpression += (jExpression === '' ? '' : ';') + key + '#<=#' + dataType + encode(end.getUTCFullYear().toString() + '-' + (end.getUTCMonth() + 1).toString() + '-' + end.getUTCDate().toString() + ' 23:59:59.999');
        }
    }

    if (!isNullOrEmpty(predefFilter) && isNullOrEmpty(begin) && isNullOrEmpty(end)) {
        jExpression += (jExpression === '' ? '' : ';') + key + '#==#' + dataType + '$' + encode(predefFilter) + '$';
        if (predefFilter[0] == 'X' && isNullOrEmpty(predefValue))
            return 'error: Favor informar o valor do parâmetro.';
        if (predefFilter[0] == 'X')
            jExpression += encode(predefValue);
    }
    return jExpression;
}
function PrepareDateToSearch(dateInstance) {
    dateInstance = getUTCDate(dateInstance);
    if (dateInstance.getMilliseconds() != 0) dateInstance.setMilliseconds(0)
    var intervals = {
        initialDate: new Date(dateInstance),
        finalDate: new Date(dateInstance)
    };
    if (dateInstance.getSeconds() == 0) {
        intervals.finalDate.setSeconds(59);
        intervals.finalDate.setMilliseconds(999);
    }
    if (dateInstance.getMinutes() == 0 && dateInstance.getSeconds() == 0) {
        intervals.finalDate.setMinutes(59);
    }
    if (dateInstance.getHours() == 0 && dateInstance.getMinutes() == 0 && dateInstance.getSeconds() == 0) {
        intervals.finalDate.setHours(23);
    }
    return intervals;
}
function formatUTCDateToString(dateInstance) {
    var formattedDate = encode(
        dateInstance.getFullYear().toString() + '-' + (dateInstance.getMonth() + 1).padLeft(2, '0') + '-' + dateInstance.getDate().padLeft(2, '0') + ' ' +
        dateInstance.getHours().padLeft(2, '0') + ':' + dateInstance.getMinutes().padLeft(2, '0') + ':' + dateInstance.getSeconds().padLeft(2, '0') + '.' + dateInstance.getMilliseconds().padLeft(3, '0')
    );

    return formattedDate;
}
function removeJExpressionSpecialChars(value) {
    if (!isNullOrEmpty(value)) {
        //value = stringReplace(value, '#', '');
        //value = stringReplace(value, '{', '');
        //value = stringReplace(value, '}', '');
        value = stringReplace(value, '---', '');
        value = stringReplace(value, ':::', '');
        //value = stringReplace(value, '|', '');
    }
    return value;
}

function stringReplace(value, from, to) {
    if (!isNullOrEmpty(value)) {
        while (value.indexOf(from) !== -1) { value = value.replace(from, to); };
    }
    return value;
}


function pad(n, width, z) {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
}

function convertDateToString(date) {
    return date.getUTCFullYear().toString() + '-' + pad((date.getUTCMonth() + 1).toString(), 2) + '-' + pad(date.getUTCDate().toString(), 2) + ' ' + pad(date.getUTCHours(), 2) + ':' + pad(date.getUTCMinutes(), 2) + ':' + pad(date.getUTCSeconds(), 2) + '.' + pad(date.getUTCMilliseconds(), 3)
}

//#endregion JExpression

//#region Wizard Methods
function wizardStepChange(controlName, navigation, index) {
    if (typeof (controlName) === 'undefined' || controlName.length == 0)
        throw new exception('controlName is null or empty.')
    if (controlName[0] !== '#')
        controlName = '#' + controlName;

    var controlInst = $(controlName);
    var total = navigation.find('li').length;
    var current = index + 1;
    // set wizard title
    $('.step-title', controlInst).text('Step ' + (index + 1) + ' de ' + total);
    // set done steps
    jQuery('li', controlInst).removeClass("done");
    var li_list = navigation.find('li');
    for (var i = 0; i < index; i++) {
        jQuery(li_list[i]).addClass("done");
    }

    if (current == 1) {
        controlInst.find('.button-previous').hide();
    } else {
        controlInst.find('.button-previous').show();
    }

    if (current >= total) {
        controlInst.find('.button-next').hide();
        controlInst.find('.button-submit').show();
    } else {
        controlInst.find('.button-next').show();
        controlInst.find('.button-submit').hide();
    }
}
//#endregion

//#region Auxiliar methods
function getDataPrimaryKeys(metadata) {
    var primaryKeys = [];

    if (metadata) {
        for (var idx in metadata) {
            if (metadata[idx].isPartOfKey) {
                primaryKeys.push(metadata[idx].key);
            }
        }
    }

    return primaryKeys;
}

function selectGridRow(grid, rowIndex, columnIndex) {
    if (!grid || (!rowIndex && rowIndex != 0) || (!columnIndex && columnIndex != 0))
        return;

    grid.igGridSelection("selectRow", rowIndex);
    grid.igGridSelection("selectCell", rowIndex, columnIndex);
    grid.igGridUpdating("startEdit", rowIndex, columnIndex);
};
function isFunction(functionToCheck) {
    var getType = {};
    return functionToCheck && getType.toString.call(functionToCheck) === '[object Function]';
}

function findIndexByKey(dataItems, key, value) {
    if (dataItems && !isNullOrEmpty(key)) {
        var items = dataItems;
        if (isFunction(items))
            items = items();
        for (var idx = 0; idx < items.length; idx++) {
            var entityValue = items[idx][key];
            if (isFunction(entityValue))
                entityValue = entityValue();
            if (entityValue == value)
                return idx;
        }
    }
    return -1;
}

function findElementByKey(dataItems, key, value) {
    if (dataItems && !isNullOrEmpty(key)) {
        var items = dataItems;
        if (isFunction(items))
            items = items();
        for (var idx = 0; idx < items.length; idx++) {
            var entityValue = items[idx][key];
            if (isFunction(entityValue))
                entityValue = entityValue();
            if (entityValue == value)
                return items[idx];
        }
    }
    return null;
}

var valueGrouBy = -1;
function formatAndAlignNumber(grid, val, record, dataType, format) {
    var isGroup = (typeof grid.data().igGridGroupBy == 'object' && grid.data().igGridGroupBy._isgroup);
    if (isGroup && (typeof record != 'undefined')) {
        if (valueGrouBy != record.RowDataId) {
            valueGrouBy = record.RowDataId;
            return val;
        }
    }

    try {
        return "<span style='text-align: right; display: inline-block;width: 100%;'>" + $.ig.formatter((typeof val === 'string' ? eval(val) : val) / (format === 'percent' ? 100 : 1), dataType, format, true, true, null) + "</span>";
    } catch (e) {
        console.error('Error in eval [' + val.toString() + '], function: formatAndAlignNumber(grid, val, record, dataType, format)')
        return val;
    }
}

function getNew_zIndex() {
    return require('plugins/dialog').getNextZIndex();
}
//#endregion Auxiliar methods

//#region CreateViewInfo
function createViewInfo(metadata, dataView, goToIndex) {
    if ($("#selectorList") !== null) {
        $("#selectorList").remove();
    }

    var pKey = 'RowDataId';
    var metaColumns = [{ key: pKey, headerText: pKey, width: '1%', dataType: 'number', hidden: true }];
    for (var idx = 0; idx < metadata.length; idx++) {
        metaColumns.push(metadata[idx]);
    };

    var selector = $('<div/>').attr({ id: 'selectorList' });
    var divTable = $('<div />').attr({ id: 'divSelectorTableInfo' }).css({ height: $(window).height() * 0.75 });
    var table = $('<table tabIndex="1" />').attr({ id: 'selectorTableInfo' });
    var rightDiv = $('<div />').attr({ id: 'selectorDiv' }).addClass('title-lookUp');
    var okButton = $('<button class="btn tooltips" ><i class="icon-ok"></i> Aplicar Seleção</button>').attr({ id: 'selectorOkButton' });
    selector.append(rightDiv);
    selector.append(divTable);
    divTable.append(table);
    rightDiv.append(okButton);
    okButton.click(function () {
        selector.igDialog("close");
        var activeRow = table.igGrid("activeRow");
        if (activeRow != null) {
            var idx = findIndexByKey(dataView(), pKey, activeRow.id);
            if (idx > -1) {
                goToIndex(idx)
            }
        }
    });
    $('body').append(selector);
    table.igGrid({
        width: '100%',
        height: '100%',
        primaryKey: pKey,
        autoGenerateColumns: false,
        enableUTCDates: true,
        dataSource: ko.mapping.toJS(dataView),
        columns: metaColumns,
        features: [
            { name: 'Selection', mode: 'row' },
            { name: "Sorting", type: "local" },
            { name: "Resizing" }
        ],
        dataRendered: function (ui, evt) {
            $('#divSelectorTableInfo').focus(1);
            $('#divSelectorTableInfo').children().focus(1);


            $('#selectorTableInfo_container').keyup(function (e) {
                if (e.keyCode == 13)
                    okButton.click();
            });
        }
    });

    table.delegate('.ui-iggrid-activerow', 'dblclick', function (e) {
        okButton.click();
    });

    selector.igDialog({
        headerText: 'Visão Tabular',
        width: '90%',
        height: $(window).height() * 0.9,
        state: 'opened',
        modal: true,
        draggable: true,
        resizable: true,
        enableHeaderFocus: false,
        zIndex: getNew_zIndex()
    });
}
//#endregion CreateViewInfo

//#region CreateAlertModal
function showModalAlert(titleError, listErrors) {
    if ($("#dialogError") !== null) {
        $("#dialogError").remove();
    }

    var listaErrosDefault = '';
    var listaErros = '';
    var dialogError = $('<div/>').attr({ id: 'dialogError' }).css("margin", "-10px 10px 0 10px");
    var divRow = $('<div/>').addClass('row');
    var divGroup = $('<div/>').addClass('span4 collapse-group');
    var hTitle = $('<h3 class="title-error">' + titleError + '</h3>');
    var btnExp = $('<a class="btn lin showdetails btn-modal btn-style" data-toggle="collapse" data-target="#viewdetailsHidden"></a>');
    var divDetailsDefault = $('<p/>').attr({ id: 'viewdetailsDefault' }).addClass('viewdetails');
    var divDetails = $('<p/>').attr({ id: 'viewdetailsHidden' }).addClass('collapse');
    var btnOk = $('<div class="modal-footer" ><button class="btn btn-ok autofocus" >Ok</button></div>').attr({ id: 'OkButton' });

    listErrors.forEach(function (entry, index) {
        if (index > 1)
            listaErros += '- ' + entry + '<br/>';
        else
            listaErrosDefault += '- ' + entry + '<br/>';
    });

    dialogError.append(divRow);
    divRow.append(divGroup);
    divGroup.append(hTitle);
    divDetailsDefault.append(listaErrosDefault);
    divGroup.append(divDetailsDefault);
    divGroup.append(divDetails);
    if (listErrors.length > 2)
        divGroup.append(btnExp);

    dialogError.append(btnOk);

    btnExp.click(function () {
        divDetailsDefault.append(divDetails);
        divDetails.html(listaErros);
        $(this).toggleClass('swop');
    });

    btnOk.click(function () {
        dialogError.dialog('close');
    });

    dialogError.dialog({
        modal: true,
        width: '40%',
        //height: 300,
        show: { effect: 'drop', direction: 'up' },
        draggable: true,
        resizable: true,
        zIndex: getNew_zIndex()
    });

    dialogError.dialog('widget').find('.ui-dialog-titlebar').hide();

}
//#endregion CreateAlertModal

//#region Multimedia
function loadMultimidiaUrl(tableName, value, vm) {
    var multimidiaService = vm.getServiceAddress('LinxFrameworkMultimidia');
    var url = vm.getServiceAddress('') + "image/no-image.png";

    if (typeof (value) == 'function')
        value = value();
    if (typeof (value) === "string" && value.length != 36) {
        value = parseInt(value);
    }

    if (!tableName || !value || (typeof (value) === "string" && value === "00000000-0000-0000-0000-000000000000") || value === 0)
        return url;

    if (value !== null && value !== '') {
        var uidKey = null;
        var idKey = null;

        if (typeof (value) === "string") {
            uidKey = value;
        }
        else {
            idKey = value;
        }

        // problema de cache no browser, funcao desabilitada
        //if (vm.managerAuth.imageServiceBus.length > 0) {

        //    //http://localhost:59914/ux-id-2/PRD_SKU_PRODUTO/id-pk/141083.jpg
        //    if (idKey != null)
        //        url = vm.managerAuth.imageServiceBus + 'ux-id-' + vm.managerAuth.getEnvironmentId() + '/' + tableName + '/id-pk/' + idKey + '.png?w=64' + "&nocache=" + Math.uuid(15);
        //    else
        //        url = vm.managerAuth.imageServiceBus + 'ux-id-' + vm.managerAuth.getEnvironmentId() + '/' + tableName + '/uid-pk/' + uidKey + '.png?w=64' + "&nocache=" + Math.uuid(15);

        //}
        //else{
        url = multimidiaService + "/getMediaThumbnailByKey?nomeTabela=" + tableName + "&idChave=" + idKey + "&uidChave=" + uidKey + "&uidGrupoAcesso=00000000-0000-0000-0000-000000000000&uidEmpresa=" + vm.managerAuth.getCompanyId() + "&uidGrupoEconomico=" + vm.managerAuth.loginInfo.UidGrupoEconomico + "&idAmbiente=" + vm.managerAuth.getEnvironmentId() + "&uidUsuario=" + vm.managerAuth.loginInfo.UidUsuario + "&" + vm.managerAuth.META_HASH + "&nocache=" + Math.uuid(15);

        // http://localhost:1710/LinxFrameworkMultimidia/GetMedia?uidDocumento=06220397-d808-4768-aa26-1028dc3c7b7f&uidGrupoAcesso=f09bbc01-ce40-456d-a284-41a51745c576&uidEmpresa=f27ffc4f-eb6e-4484-91ed-a318a4a394b0&uidGrupoEconomico=f27ffc4f-eb6e-4484-91ed-a318a4a394b0&idAmbiente=2
        //url = multimidiaService + "/getMediaByKey?nomeTabela=" + tableName + "&idChave=" + idKey + "&uidChave=" + uidKey + "&uidGrupoAcesso=00000000-0000-0000-0000-000000000000&uidEmpresa=" + vm.managerAuth.getCompanyId() + "&uidGrupoEconomico=" + vm.managerAuth.loginInfo.UidGrupoEconomico + "&idAmbiente=" + vm.managerAuth.getEnvironmentId() + "&" + vm.managerAuth.META_HASH + "&nocache=" + Math.uuid(15);
        //}

    }
    return url;
}

function showMultimidia(data, event, tableName, value, vm) {
    var e = event.toElement;

    if (e == null)
        e = event;

    if (vm.status() !== 'C') {
        vm.modalMultimidia.showModal(tableName, value, vm, data).then(function (r) {
            data.UI_MULTIMIDIA_VM = r.UI_MULTIMIDIA_VM;

            //event.toElement.src
            if (r.UidDocumentos.length > 0) {
                setAbsoluteValue(data, 'TableMedia', r.UidDocumentos);
            }

            if (r.UrlThumbnail.length > 0) {
                if (e.srcElement == null)
                    e.src = r.UrlThumbnail;
                else
                    e.srcElement.src = r.UrlThumbnail;
            }
        });
    }
}

function showMultimidiaLazy(name) {
    $(name + ' > tbody > tr > td > div > img.lx_grid_linkimg').lazy({
        bind: "event",
        delay: 0,
        //visibleOnly: true,
        afterLoad: function (element) {
            var landscapes = $('.tabbable-custom .lx_grid_linkimg');
            landscapes.each(function (index, element) {
                if (element.height > element.width) {
                    $(element).addClass('fitHeightInContent');
                } else {
                    $(element).addClass('fitWidthInContent');
                }
            })
        },
        onError: function (element) {
            console.log("image loading error: " + element.attr("data-src"));
        },
    });
}

var KO_afterRenderImageTemplate = function (data, element, tableName, key, entity, vm) {
    var templateName = '';

    if (vm.status() !== 'C')
        templateName = "image-template-form";
    else
        templateName = "no-image-template-form";

    if (templateName == 'no-image-template')
        return;

    var url = loadMultimidiaUrl(tableName, key, vm);
    var e = $(element[1]);

    $(e).attr('data-src', url)

    $(e).lazy({
        bind: "event",
        attribute: "data-src",

        afterLoad: function (element) {

            var landscapes = $('.portlet-body .lx_linkimg');

            landscapes.each(function (index, element) {
                //if (element.className.indexOf('fitHeightInContent') == -1 && element.className.indexOf('fitWidthInContent') == -1) {

                if (element.height > element.width) {
                    $(element).addClass('fitHeightInContent');
                }
                else {
                    $(element).addClass('fitWidthInContent');
                }
                //}
            })
        },

        onError: function (element) {
            console.log("image loading error: " + element.attr("data-src"));
        }
    });

    $(e).click(function () {
        showMultimidia(data, this, tableName, key, vm);
    });

}

ko.renderTemplateX = function (name, vm, data) {
    // create temporary container for rendered html
    var temp = $("<div>");
    // apply "template" binding to div with specified data
    ko.applyBindingsToNode(temp[0], {
        template: {
            name: name,
            data: data
        }
    },
        vm
    );
    // save inner html of temporary div
    var html = temp.html();
    // cleanup temporary node and return the result
    temp.remove();
    return html;
};

var getTemplateImageName = function (status, type, size) {
    if (status !== 'C')
        return "image-template-" + type
    else
        return "no-image-template-" + type
};

//#endregion Multimedia

//#region globalDataParameters
var globalDataParameters = {
    parameters: [],
    registerParameters: function (system, managerAuth, logger, parameterList) {
        var dfd = $.Deferred();

        var common = require('common')
        var cacheKey = common.getCachePrefixEnvironment('API', 'GetParameterValue', managerAuth.loginInfo.CacheKey);
        var cacheValue = $.ezstorage.get(cacheKey);

        if (parameterList !== '') {
            if (cacheValue == null) {

                var environmentInfo = [];
                for (var i = 0; i < managerAuth.loginInfo.Ambientes.length; i++) {
                    var item = managerAuth.loginInfo.Ambientes[i];
                    environmentInfo.push({ Hash: '', EnvironmentId: item.IdTcsAmbiente, ApplicationUid: item.UidAplicacao, CompanyUid: item.UidEmpresa, AplicativeId: item.IdTcsAplicativo, ParameterList: parameterList });
                }

                system.log('Main: Loading Config...');
                return $.ajax({
                    type: 'POST',
                    message: "Buscando configurações",
                    messageUser: "Accesso as configurações/parametros do sistema",
                    headers: {
                        'CurrentUser': managerAuth.loginInfo.UidUsuario,
                        'EconomicGroup': managerAuth.loginInfo.UidGrupoEconomico,
                        'LoginMode': managerAuth.loginMode
                    },
                    globalError: true,
                    url: managerAuth.getServiceAddress('LinxFrameworkParametro', 'Linx.Framework.BV') + '/GetParameterValueMultiEnvironment',
                    data: JSON.stringify(environmentInfo),
                    contentType: "application/json",
                    cache: false,
                    error: function (jqXHR, textStatus, errorThrown) {
                        //var msg = 'Error getting the following Parameters: [' + parameterList + ']';
                        //logger.logError(msg, errorThrown, 'GET Fail', true);

                        dfd.fail(errorThrown);
                    },
                    success: function (data) {
                        if (managerAuth.isShellDevMode)
                            $.ezstorage.set(cacheKey, data, { expires: 90 })
                        else
                            $.ezstorage.set(cacheKey, data)

                        managerAuth.loadParameters(data);

                        dfd.promise();
                    }
                });
            }
            else {
                system.log('Main: Loading Config... [Storage]');
                managerAuth.loadParameters(cacheValue);
                return dfd.resolve();
            }
        }
    },
    getAllParameter: function (parameterList, managerAuth) {
        var dfd = $.Deferred();

        var environmentInfo = [];
        for (var i = 0; i < managerAuth.loginInfo.Ambientes.length; i++) {
            var item = managerAuth.loginInfo.Ambientes[i];
            environmentInfo.push({ Hash: '', EnvironmentId: item.IdTcsAmbiente, ApplicationUid: item.UidAplicacao, CompanyUid: item.UidEmpresa, AplicativeId: item.IdTcsAplicativo, ParameterList: parameterList });
        }

        return $.ajax({
            type: 'POST',
            message: "Buscando configurações",
            messageUser: "Accesso as configurações/parametros do sistema",
            headers: {
                'CurrentUser': managerAuth.loginInfo.UidUsuario,
                'EconomicGroup': managerAuth.loginInfo.UidGrupoEconomico,
                'LoginMode': managerAuth.loginMode
            },
            globalError: true,
            url: managerAuth.getServiceAddress('LinxFrameworkParametro', 'Linx.Framework.BV') + '/GetParameterValueMultiEnvironment',
            data: JSON.stringify(environmentInfo),
            contentType: "application/json",
            cache: false,
            error: function (jqXHR, textStatus, errorThrown) {
                console.error(errorThrown);
                dfd.fail(errorThrown);
            },
            success: function (data) {
                return dfd.resolve(data);
            }
        });
    },

    //getParameter: function (parameterList, managerAuth, callback) {
    //    var dfd = $.Deferred();

    //    return this.getAllParameter(parameterList, managerAuth).then(function (data) {
    //        if (data.length === 1) {
    //            if (callback)
    //                callback(data[0].ValorParametro);
    //            return dfd.resolve(data[0].ValorParametro)
    //        }
    //        else
    //            return dfd.resolve(data);
    //    });
    //}

        getParameter: function (parameterName, managerAuth, callback) {
        var dfd = $.Deferred();
        return $.ajax({
            type: 'GET',
            message: "Buscando parametro",
            messageUser: "Accesso as configurações/parametros do sistema",
            headers: managerAuth.getHeaders(),
            globalError: true,
            url: managerAuth.getServiceAddress('LinxFrameworkParametro', 'Linx.Framework.BV') + '/GetParameterValue?serializedParameterList=' + parameterName,
            dataType: 'json',
            cache: false,
            error: function (jqXHR, textStatus, errorThrown) {
                console.error(errorThrown);
                dfd.resolve();
            },
            success: function (data) {
                var parametersName = '';
                var parameters = data.split('#');
                for (var idx in parameters) {
                    var values = parameters[idx].split('|');
                    if (callback)
                        callback(values[1]);
                }
                dfd.promise();
            }
        });
    }

};
//#endregion globalDataParameters

//#region EditorProviderMultimedia
$.ig.EditorProviderMultimedia = $.ig.EditorProviderMultimedia || $.ig.EditorProvider.extend(
    {
        createEditor: function (updating, key, columnSetting, tabIndex, format, dataType, cellValue) {
            var element, settings = {};
            if (columnSetting) {
                settings = columnSetting.editorOptions || settings;
            }
            settings.change = function () {
                updating._notifyChanged();
            };

            element = $('<img />').attr({ id: 'image', tabindex: tabIndex, IdChave: key, width: 40, height: 40 });
            var tableName = columnSetting.editorOptions.tableName;
            var vm = columnSetting.editorOptions.vm;

            var gridName = columnSetting.editorOptions.gridName;
            var grid = $(gridName);
            var selectedRow = grid.igGridSelection("selectedRow");
            var dataSource = grid.igGrid("option", "dataSource");
            var primaryKeys = grid.igGrid("option", "primaryKey");
            var entity = dataSource[selectedRow.index];

            var url = loadMultimidiaUrl(tableName, entity[key], vm);
            element.prop('src', url);

            this.editor = element;
            return element;
        },
        getValue: function () {
            return this.editor.find("#image").attr('IdChave');
        },
        setValue: function (val) {
            if (this.editor.find("#image").attr('IdChave') !== val) {
                this.editor.find("#image").attr('IdChave', val);
            }
        },
        setSize: function (width, height) {
            this.editor.find('#image').css({
                width: width,
                height: height
            });

        },
        setFocus: function () {
            this.editor.find("#image").focus();
        },
        validator: function () {
            return null;
        },
        removeFromParent: function () {
            return this.editor.remove();
        },
        destroy: function () {
            this.editor.destroy();
        }
    });
//#endregion EditorProviderMultimedia

//#region lookups


function lookupInformation() {
    ///<summary>Class that contains info for a lookup</summary>

    //Field for sorting
    this.fieldToSort = "";
    this.sortDirection = "ascending";
    ///visible columns separated per comma ","
    this.visibleColumns = '';
    ///Take
    this.pageSize = 100;
    ///skip
    this.pageSkip = 0;
    ///total records
    this.totalRecords = -1;
    ///Last EntityExpression
    this.lastJEntityExpression = null;
    ///viewModel
    this.vm = null;
    ///Multi Selecion
    this.isMultiSelection = false;
    ///toal page
    this.totalPages = function () {
        return parseInt(this.totalRecords === -1 ? 0 : parseInt(this.totalRecords / this.pageSize, 10).toFixed(0), 10);
    };
    this.getCurrentDisplay = function () {
        if (this.totalRecords <= this.pageSize) {
            return ('Total: ' + this.totalRecords.toString());
        }
        else {
            var totalRecords = (this.totalPages() * this.pageSize) + 1;
            var startRecords = parseInt(this.pageSkip * this.pageSize, 10) + 1;
            var endRecords = (this.pageSkip * this.pageSize) + this.pageSize;
            if (endRecords > this.totalRecords) {
                endRecords = this.totalRecords;
            }
            return parseInt(startRecords, 10).toString() + "-" + parseInt(endRecords, 10).toString() + "/" + (this.totalRecords).toString();
        }
    };
}

function showLookUp(dataContext, currentDataItem, title, lookupName, fieldToSearch, internalLookupSearch, lookupInfo, finished, dataSource, allowMultiSelectionInSearch) {
    if (!isNullOrEmpty(dataSource) && dataSource.length == 1) {
        dataContext['finalizeAll' + lookupName](currentDataItem, dataSource, '', lookupInfo);
        if (finished)
            finished(true);
        return;
    }

    require(['viewmodels/shared/modalLookUp'],
        function (modal) {
            modal
                .showModal(title, lookupName, dataContext.metadataInfo[lookupName], fieldToSearch, internalLookupSearch, lookupInfo, dataSource, allowMultiSelectionInSearch)
                .then(function (r) {
                    if (!r.cancel) {
                        if (r.selectedItems.length > 1 && lookupInfo.vm.status() === 'C') {
                            if (finished) {
                                finished(true, r.selectedItems);
                            }
                            return;
                        } else {
                            if (r.selectedItems.length > 0) {
                                dataContext['finalizeAll' + lookupName](currentDataItem, r.selectedItems, '', lookupInfo);
                            }
                        }
                    }
                    if (finished)
                        finished(!r.cancel);
                });
        });
}
//#endregion lookups

//#region EditorProviderLookUp
$.ig.EditorProviderLookUp = $.ig.EditorProviderLookUp || Class.extend({

    createEditor: function (updating, key, columnSetting, tabIndex, format, dataType, cellValue) {
        var _this = this, column, settings = {};
        if (columnSetting) {
            settings = columnSetting.editorOptions || settings;
        }

        for (var i = 0; i < updating.grid.options.columns.length; i++)
            if (updating.grid.options.columns[i].key === columnSetting.columnKey)
                column = updating.grid.options.columns[i];

        var custom = settings.custom;
        var updateValue = function (event, args) {
            updating._notifyChanged();
            if (args && args.value && !isNullOrEmpty(args.value))
                _this.setValue(args.value);
        }
        var opt = {};
        if (settings.readOnly)
            opt.disabled = settings.readOnly;
        opt.lookupName = settings.lookUpName;
        opt.fieldName = key;
        opt.isNullable = settings.isNullable;
        opt.textChanged = updateValue;
        opt.lookupValueChanged = updateValue;
        opt.keyDown = function (event, args) { updating._notifyEditorKey(event, key); };
        opt.isDataColumnGrid = true;
        opt.vm = settings.vm;
        opt.entity = function () {
            return findElementByKey(updating.grid.dataSource.settings.dataSource, 'RowDataId', updating._row_);
        };
        opt.css = updating.bindings[0].className;
        opt.width = column.width;
        opt.allowMultiSelectionInSearch = settings.allowMultiSelectionInSearch;
        opt.enableAutoComplete = settings.activateAutoComplete;
        opt.validateOnClearState = settings.validateOnClearState;
        opt.autoCompleteMinLength = settings.autoCompleteMinLength;
        opt.autoCompleteMaxResults = settings.autoCompleteMaxResults;
        opt.maxValue = settings.maxValue;
        opt.maxLength = settings.maxLength;
        opt.getRowID = function () {
            return updating._row_;
        }
        //opt.tabIndex = tabIndex;
        opt.dataColumnGridFocus = function (rowIndex) {
            var grid = $(updating.element[0]);
            var colIndex = 0;
            var colsVisible = grid.data('igGridUpdating').grid._visibleColumnsArray;
            var canEditingOneField = false;
            var colIndexVisible = 0;
            var entity = findElementByKey(updating.grid.dataSource.settings.dataSource, 'RowDataId', rowIndex);
            if (colsVisible.length > 1) {
                for (var i = 0; i < colsVisible.length; i++) {
                    var nameColumn = colsVisible[i].key;
                    canEditingOneField = (canEditingOneField == true ? canEditingOneField : settings.verifyCanEditCol(nameColumn, settings.vm.status() == 'C', entity))
                    if (nameColumn == key) colIndexVisible = i;
                }
                if (canEditingOneField) {
                    var indexColumn = colIndexVisible;
                    var canNewEditing = false;

                    for (; indexColumn < colsVisible.length;) {
                        if ((indexColumn + 1) >= colsVisible.length)
                            indexColumn = -1;
                        else {
                            var colNameVisible = colsVisible[indexColumn + 1].key;
                            canNewEditing = settings.verifyCanEditCol(colNameVisible, settings.vm.status() == 'C', entity);
                            if (canNewEditing) {

                                var validCustomDelegates = true;
                                var event = jQuery.Event('iggridupdatingeditcellstarting');
                                event.target = grid[0];

                                var ui = {};
                                ui.columnKey = colNameVisible;
                                ui.rowID = rowIndex;
                                ui.columnIndex = indexColumn + 1;
                                ui.owner = grid;
                                if (entity != null) {
                                    ui.rowAdding = entity.isAdded();
                                    ui.value = entity[ui.columnKey];
                                }

                                grid.trigger(event, ui);

                                if (event.result == undefined) event.result = true;

                                validCustomDelegates = event.result;

                                if (validCustomDelegates) {
                                    var selectedRow = null;
                                    if (grid.data('igGrid').selectedRows() != null)
                                        selectedRow = grid.data('igGrid').selectedRows();
                                    if (grid.data('igGrid').selectedRows() && grid.data('igGrid').selectedRows().length > 0)
                                        selectedRow = grid.data('igGrid').selectedRows()[0];
                                    if (selectedRow == null)
                                        selectedRow = grid.data('igGrid').activeRow();
                                    if (selectedRow == null)
                                        selectedRow = grid.igGridSelection('selectedRow');
                                    if (selectedRow != null) {
                                        grid.igGridSelection('selectRow', selectedRow.index);
                                        grid.igGridUpdating('startEdit', selectedRow.id, indexColumn + 1);
                                    }
                                    break;
                                }
                                else
                                    indexColumn++;

                            }
                            else
                                indexColumn++;
                        }
                    }
                }
            }
        };
        var div = $('<div />').attr({ id: 'lookUp' });

        div = div.lookupControl(opt);

        this.editor = div.data('lookupControl');

        return div;
    },
    getEditor: function () { return this.editor; },
    attachErrorEvents: function (errorShowing, errorShown, errorHidden) {
        this.editor.element.bind({
            "igeditorerrorhidden.updating": errorHidden,
            "igeditorerrorshowing.updating": errorShowing,
            "igeditorerrorshown.updating": errorShown
        });
    },
    getValue: function () {
        return this.editor.value();
    },
    setValue: function (val, updating) {
        if (this.editor.value() !== val) {
            this.editor.value(val);
            return val;
        }
        return false;
    },
    setFocus: function () {
        this.editor.setFocus(-1);
    },
    setSize: function (width, height) {
        //this.editor._refresh();
    },
    removeFromParent: function () {
        return this.editor.remove();
    },
    destroy: function () {
        this.editor.destroy();
    },
    validator: function () { return null; },
    validate: function (noLabel) {
        var validator = this.validator();
        return validator ? !validator.validate() : true;
    },
    keepFocus: function () {
        var validator = this.validator(), foc = validator ? validator._foc1(validator.options) : false;
        if (!foc) {
            return false;
        }
        if (foc === 2) {
            return true;
        }
        foc = validator && !validator._focTime;
        validator._focTime = new Date().getTime();
        return foc;
    },
    isValid: function () {
        var validator = this.validator();
        return validator ? validator.isValidState() : true;
    }
});
//#endregion EditorProviderLookUp

//#region lookupocu

$.widget("linx.lookupControl", {
    options: {
        //ko value
        value: '',
        isMultiSelection: false,
        //lookup Name
        lookupName: "",
        //lookupField
        fieldName: "",
        //viewModel
        vm: null,
        //entity
        entity: null,

        height: 26,
        width: 0,

        enableAutoComplete: false,
        autoCompleteMaxResults: 7,
        autoCompleteMinLength: 3,

        isNullable: true,
        isMultiValue: false,
        validateOnClearState: false,
        allowMultiSelectionInSearch: true,


        tabIndex: null,
        getRowID: function () { return 1; },
        disabled: false,
        isDataColumnGrid: false,
        // callbacks
        lookupValueChanging: null,
        lookupValueChanged: null,
        textChanged: null,
        keyDown: null,
        inputClass: null,
        maxLength: 10000,
        maxValue: 0,
        dataColumnGridFocus: function () { },
        defaultValue: ''
    },
    css: {
        editor: '',
        disabled: ''
    },
    processing: false,
    _create: function () {
        var _this = this, o = this.options;

        this._validateLookup();
        _this.input = $("<input id=" + _this.bindings[0].id + "_inputLookUp" + " /> ")
            .val(o.value)
            .appendTo(_this.element);

        if (!isNullOrEmpty(o.inputClass))
            _this.input.addClass(o.inputClass);

        if (!o.isDataColumnGrid) {
            _this.input.addClass("form-control ellipsis");
        } else {
            _this.element.addClass("input-group");
        }
        _this.element.append(' ');

        _this.button = $("<button />", { title: "Pesquisar", "class": "input-group-addon" })
            .append($('<i />').addClass('icon-search'))
            .appendTo(_this.element);

        _this.button
            .mousedown(function () {
                _this._isMouseDown = true;
            })
            .mouseup(function () {
                _this._isMouseDown = false;
            });



        if (!o.tabIndex) {
            o.tabIndex = _this.element.prop('tabIndex');
        }
        if (o.tabIndex && o.tabIndex > 0) {
            _this.element.children().prop('tabIndex', o.tabIndex);
        }

        _this._on(_this.input, { change: "_textChanged", keydown: "_keyDown", keyup: "_keyUp", blur: "_blur" });
        _this._on(_this.button, { click: "_click", keydown: "_buttonKeyDown" });

        if (o.enableAutoComplete) {
            _this.input.autocomplete({
                maxResults: o.autoCompleteMaxResults,
                minLength: o.autoCompleteMinLength,
                delay: 300,
                autoFocus: true,
                select: function (event, ui) {
                    _this.executeLookup();
                },
                source: function (request, response) {
                    var extrafilter = _this._getEntity().canGetClientFilter(o.lookupName) ? _this._getEntity().getLookUpClientFilterExpressions(o.lookupName, null) : '';

                    //BeforeGetLookup
                    if (typeof _this._getEntity()['BeforeGet' + o.lookupName + 'Query'] == 'function') {
                        var customFilter = _this._getEntity()['BeforeGet' + o.lookupName + 'Query'](o.fieldToSearch, o.lookupInfo);
                        if (customFilter !== 'Error' && !isNullOrEmpty(customFilter)) { extrafilter = (isNullOrEmpty(extrafilter) ? '' : extrafilter + ';') + customFilter; }
                    }

                    var fName = _this._getEntity().getLookupPropertyName(_this.getFieldName());
                    var jExp = o.lookupName + '{' + extrafilter + (isNullOrEmpty(extrafilter) ? '' : ';') + fName + '#Like#S' + request.term + '%}';
                    o.vm.getDataContext()['get' + o.lookupName + 'ByEntitySearch'](jExp, fName, 0, o.autoCompleteMaxResults, null, fName)
                        .then(function (data) {
                            var list = data.results.map(function (i) { return { label: i[fName], value: i[fName] }; });
                            if (data.inlineCount > o.autoCompleteMaxResults)
                                list.push({ label: '[' + o.autoCompleteMaxResults + ' de ' + data.inlineCount + ']', value: request.term + '%' });
                            response(list);

                        });
                },
                //create: function () {
                //    _this.input.data('ui-autocomplete')._renderItem = function (ul, item) {
                //        return $("<li>")
                //          .addClass("Please work")
                //          .attr("data-value", item)
                //          .append(item[o.fieldName])
                //          .appendTo(ul);
                //    };

                //}

            });
        }

        var touchStart = function (e) {
            if (_this.input.is(':focus')) _this._click();
        };
        _this.input.bind("touchstart MSPointerDown", touchStart);
    },
    _refresh: function () {

        var width = this.__getSize(this.options.width);
        if (width === 0) {
            width = this.__getSize(this.element.css('width'));
        }
        if (width > 0 && isNullOrEmpty(this.options.inputClass)) {
            this.input.css('width', (width - 30));
        }
        if (this.options.isDataColumnGrid)
            this.element.css('width', width);

        this.button.css('height', this.options.height);
        this.input.css('height', this.options.height);
        this.element.css('height', this.options.height);


        var disabled = false;
        if (typeof (this.options.disabled) == 'function')
            disabled = this.options.disabled();
        disabled = disabled === true || this.options.disabled === true || this.element[0].disabled || this.element[0].readOnly;

        if (disabled)
            this.button.addClass('btn-remove-hover');
        else
            this.button.removeClass('btn-remove-hover');

        this.options.disabled = disabled;
        this.element.prop('readOnly', disabled);
        this.input.attr("readOnly", disabled);
        this.button.prop('disabled', disabled);

        this.button.css('cursor', (disabled ? 'not-allowed' : 'pointer'));
    },
    _blockButton: function (block) {
        this._locked = block;
        this.button.prop('disabled', block);
    },
    _canExecute: function () {
        var _this = this;
        if (_this._locked) return false;
        _this._blockButton(true);
        setTimeout(function () { _this._blockButton(false); }, 1000);
        return true;
    },
    setFocus: function (delay) {
        var _this = this;
        if (isNullOrEmpty(delay))
            delay = 200;

        setTimeout(function () { _this.input[0].focus(); }, delay);
    },
    _lastText: null, _lastSearchedText: '', _locked: false, _isClear: true,
    _textChanged: function () {
        var _this = this;
        var actualValue = _this._getVal();
        if (_this._lastText != actualValue) {
            _this._isChanged = true;
            _this._lastText = actualValue;
            _this.input.attr("title", actualValue);
        }

        setTimeout(function () {
            var throwTrigger = true;
            if (isNullOrEmpty(_this._getVal())) {
                _this._setVal(_this.options.defaultValue, true);
                throwTrigger = false;
            }

            if (_this.isValidText(actualValue) && throwTrigger) {
                _this._trigger("textChanged", $.Event('textChanged'), { value: actualValue });
            }
            else {
                _this._setVal(_this.options.defaultValue, true);
            }
        }, 100);

        try { _this._trigger("textChanged", $.Event('textChanged')); }
        catch (e) { console.warn(e); }
    },
    isValidText: function (searchValue) {

        if (!isNullOrEmpty(searchValue) &&
            ((searchValue.trimStart().startsWith('[') && !searchValue.trimEnd().endsWith(']'))
                || (!searchValue.trimStart().startsWith('[') && searchValue.trimEnd().endsWith(']')))) {
            require('durandal/app').showMessage('O formato está incorreto para pesquisar vários valores. Formato esperado: [valor1,valor2,...,valorN].', 'Alerta', ['Ok']);
            return false;
        }

        if (this._isMultiSelectionSearch(searchValue)) {
            if (searchValue.replaceAll(' ', '').contains(',,') || searchValue.replaceAll(' ', '').contains(',]') || searchValue.replaceAll(' ', '').contains('[,') || searchValue.replaceAll(' ', '').contains('[]')) {
                require('durandal/app').showMessage('O formato está incorreto para pesquisar vários valores. Formato esperado: [valor1,valor2,...,valorN].', 'Alerta', ['Ok']);
                return false;
            }
            if (this._getEntity() != null && 'LHIYDF'.indexOf(this._getEntity().serverDataType[this.options.fieldName]) >= 0) {
                var values = searchValue.replace('[', '').replace(']', '').split(',');
                for (var i = 0; i < values.length; i++) {
                    if (isNaN(values[i])) {
                        require('durandal/app').showMessage('Não foi possível converter o valor [' + values[i] + '] para numérico. Formato esperado: [1,2,...,10].', 'Alerta', ['Ok']);
                        return false;
                    }
                }
            }
        } else if (this._getEntity() != null && 'LHIYDF'.indexOf(this._getEntity().serverDataType[this.options.fieldName]) >= 0 && isNaN(searchValue)) {
            require('durandal/app').showMessage('Não foi possível converter o valor [' + searchValue + '] para numérico.', 'Alerta', ['Ok']);
            return false;
        }


        return true;
    },
    _isMultiSelectionSearch: function (searchValue) {
        return !isNullOrEmpty(searchValue) && searchValue.toString().trimStart().startsWith('[');
    },
    _invalidateLookup: function () {
        var ownerReference = this._getEntity();
        if (ownerReference && ownerReference.validatedlookupsArray && ownerReference.validatedlookupsArray.contains(this.options.lookupName))
            ownerReference.validatedlookupsArray.removeItem(this.options.lookupName);
    },
    _validateLookup: function () {
        var ownerReference = this._getEntity();
        if (ownerReference && ownerReference.validatedlookupsArray && !ownerReference.validatedlookupsArray.contains(this.options.lookupName))
            ownerReference.validatedlookupsArray.push(this.options.lookupName);
    },
    _isValidatedLookup: function () {
        var ownerReference = this._getEntity();
        return ownerReference && ownerReference.validatedlookupsArray && ownerReference.validatedlookupsArray.contains(this.options.lookupName);
    },
    _buttonKeyDown: function (evt, ui) {
        if (this.options.keyDown)
            this.options.keyDown(evt);
    },
    _keyDown: function (evt, ui) {
        if (evt.keyCode === 13) {
            if (this.options.isDataColumnGrid && !this.processing) {
                var _this = this, _entity = _this._getEntity();
                setTimeout(function () {
                    _this._internalExecuteLookup(_entity);
                }, 500);
            }
        } else {
            if (evt.keyCode !== 9) {
                this._invalidateLookup();
                this._isClear = false;
            }
        }

    },
    _keyUp: function (evt, ui) {
        var actualValue = this._getVal();
        //fix maxLength
        var maxLength = this._isMultiSelectionSearch(actualValue) ? 10000 : this.options.maxLength;
        if (maxLength > 0 && this.input.prop('maxlength') != maxLength) {
            this.input.prop('maxlength', maxLength);
        }

        this._fixMaxValue(actualValue);

    },
    _fixMaxValue: function (actualValue) {
        if (isNullOrEmpty(actualValue)) actualValue = this._getVal();
        if (!isNaN(actualValue) && !this._isMultiSelectionSearch(actualValue) && !isNullOrEmpty(this.options.maxValue) && !isNaN(this.options.maxValue) && this.options.maxValue > 0) {
            try {
                if (parseInt(actualValue) > parseInt(this.options.maxValue))
                    this._setVal(this.options.maxValue.toString());
            }
            catch (e) {
                this._setVal(this.options.maxValue.toString());
            }
        }
    },
    _blur: function () {
        this._internalBlur();
        this._lastSearchedText = this.value();
    },
    _internalBlur: function () {
        var _this = this, _entity = _this._getEntity();

        if (_this.processing)
            return false;

        if (_this._canThrowClear()) {
            _this.clear();
            return false;
        }

        if (_this.options.vm.status() === 'C' && (!_this.options.validateOnClearState || (_this.options.validateOnClearState && (isNullOrEmpty(this.value()) || this.value() === '0'))))
            return false;

        if (_this._canThrowLookup()) {
            setTimeout(function () {
                _this._internalExecuteLookup(_entity);
            }, 100);
        }
    },
    _canThrowClear: function () {
        var isTextEmpty = this._isChanged && (isNullOrEmpty(this.value()) || this.value() === '0');
        return isTextEmpty && !this._isClear && !this._isValidatedLookup();
    },
    _canThrowLookup: function () {
        var actualValue = this.value();
        var hasValue = !(isNullOrEmpty(actualValue) || actualValue === '0');
        var isNullable = this.options.isNullable === true;

        var isTextChanged = !this._isValidatedLookup() && hasValue;
        var throwIfIsNullable = isNullable && isTextChanged && hasValue;
        var throwIfIsNotNullable = !isNullable && (isTextChanged || !hasValue);

        return throwIfIsNullable || throwIfIsNotNullable || isTextChanged;
    },
    clear: function () {
        var _this = this, o = _this.options;
        if (!this._canExecute()) return;
        _this._trigger("lookupValueClearing");

        if (_this._getEntity() != null && _this._getEntity().clearLookUp != undefined)
            _this._getEntity().clearLookUp(o.lookupName);

        _this._invalidateLookup();
        _this._isChanged = false;
        _this._isClear = true;

        var fieldName = (typeof this.options.fieldName == 'function') ? this.options.fieldName() : this.options.fieldName;
        if (this.options.vm.entitySearchRange[this._getEntity().typeName + fieldName] != null) {
            this.options.vm.entitySearchRange[this._getEntity().typeName + fieldName](null);
        }

        _this._trigger("lookupValueCleared");
    },
    _getEntity: function () {
        var entityObject = this.options.entity;
        if (typeof (entityObject) === 'function')
            entityObject = entityObject();
        return entityObject;
    },
    getFieldName: function () {
        var field = this.options.fieldName;
        if (typeof field == 'function')
            field = field();
        return field;
    },
    _internalExecuteLookup: function (entity) {
        if (!this._canExecute()) return;
        var _this = this, o = this.options, field = this.getFieldName(), rowID = o.getRowID();

        if (_this.processing || $('.modal-dialog').attr('moduleinfo') === o.vm.__moduleId__)
            return;

        _this._trigger("lookupValueChanging");

        if (o.vm.dataToolbar.isBusy()) return;

        var entityObject = entity != undefined ? entity : _this._getEntity();

        if (entityObject == null || (typeof entityObject === 'undefined') || (typeof entityObject.executeLookUp !== 'function'))
            return;

        if (!_this.isValidText(_this.value()))
            return;

        _this._fixMaxValue(_this.value());
        _this.processing = true;
        entityObject.executeLookUp(
            o.lookupName,
            field,
            (!o.vm.custom ? null : o.vm.custom.beforeGettingLookup),
            o.vm,
            _this._isValidatedLookup() ? '' : _this.value(),
            function (confirm, multiSelectionValue) {
                _this.processing = false;
                _this._lastSearchedText = _this.value();
                _this._isClear = false;
                _this._isChanged = false;

                if (o.vm.custom && o.vm.custom.afterGettingLookup)
                    o.vm.custom.afterGettingLookup({ lookupName: o.lookupName, entity: o.entity, viewModel: o.vm, userConfirm: confirm });

                if (o.isMultiValue)
                    _this.value(multiSelectionValue);

                _this._trigger("lookupValueChanged", $.Event('lookupValueChanged'), { value: isNullOrEmpty(multiSelectionValue) ? '' : multiSelectionValue });


                if (o.isDataColumnGrid && confirm) {
                    if (o.dataColumnGridFocus) o.dataColumnGridFocus(rowID);
                }
                else {
                    _this.button.focus();
                }
            },
            undefined,
            o.allowMultiSelectionInSearch,
            o.activateAutoComplete
        );
        this._lastSearchedText = this.value();
    },
    _click: function (sourceEvent) {
        var _this = this;
        setTimeout(function () {
            _this._internalExecuteLookup(null);
        }, 150);
    },
    executeLookup: function () {
        var _this = this, _entity = _this._getEntity();
        setTimeout(function () {
            _this._internalExecuteLookup(_entity);
        }, 100);
    },
    __getSize: function (size) {
        if (!isNullOrEmpty(size)) {
            if (typeof (size) == 'string') {
                size = size.replace('px', '');
                size = parseInt(size);
            }
            return size;
        }
        else
            return 0;
    },
    _destroy: function () {
        this.element.children().remove();
    },
    remove: function () {
        var p, e = this.element;
        p = (e && e[0]) ? e[0].parentNode : null;
        if (p && p.tagName) {
            p.removeChild(e[0]);
        }
        return this;
    },
    _getVal: function () {
        return this.input.val();
    },
    _setVal: function (val, force) {
        if (this._getVal() != val || force) {
            //clear EntitySearch
            var fieldName = (typeof this.options.fieldName == 'function') ? this.options.fieldName() : this.options.fieldName;
            if (!this._isMultiSelectionSearch(val) && this.options.vm.entitySearchRange[this._getEntity().typeName + fieldName] != null) {
                this.options.vm.entitySearchRange[this._getEntity().typeName + fieldName](null);
            }

            this.input.val(val);
            if (this._getEntity() && typeof this._getEntity()[fieldName] == 'function')
                this._getEntity()[fieldName](val);
        }

        this._isChanged = !isFinalizingLookup();

        if (isClearingLookup())
            this._isClear = true;
        if (isFinalizingLookup()) {
            this._isClear = false;
        }

        if (isFinalizingLookup() || isClearingLookup()) {
            this._lastSearchedText = this.value();
        }
    },
    value: function (val) {
        if (val === undefined) {
            val = this._getVal();
            return (val === undefined) ? null : val;
        }
        if (this._lastText != this._getVal())
            this._lastText = this._getVal();
        this._setVal(val, false);
        return this;
    },
    _setOptions: function () {
        this._superApply(arguments);
    },
    _setOption: function (key, value) {
        if (key === 'width') {
            this.options.width = value;
            this._refresh();
            return this;
        }
        if (key === 'height') {
            this.options.height = value;
            this._refresh();
            return this;
        }
        this._super(key, value);
        this._refresh();
        return this;
    },
    validator: function () {
        var _this = this;
        return {
            validate: function (noLabel) {
                return true;
            },
            isValidState: function () {
                return _this._isValidatedLookup();
            },
        };
    },
    validate: function (noLabel) {
        var validator = this.validator();
        return validator ? !validator.validate(noLabel) : true;
    },
    isValid: function () {
        var validator = this.validator();
        return validator ? validator.isValidState() : true;
    },
    // type: 1-mousedown, 2-click, 3-dblclick, 4-keydown, 5-focus, 6-blur, 7-mousemove, 8-mouseleave, 9-scroll, 11-touch
    _onEvt: function (e, type) {
    }
});
var _isFinalizingLookup = false;
function isFinalizingLookup(val) {
    if (typeof val !== "undefined") {
        _isFinalizingLookup = val;
        return;
    }

    if (typeof _isFinalizingLookup === "undefined")
        _isFinalizingLookup = false;

    return _isFinalizingLookup;
}



var _isClearingLookup = false;
function isClearingLookup(val) {
    if (typeof val !== "undefined") {
        _isClearingLookup = val;
        return;
    }

    if (typeof _isClearingLookup === "undefined")
        _isClearingLookup = false;

    return _isClearingLookup;
}
function getSetHelper(property, newValue, element, entity, bindingHandlerName, bindingFieldName) {
    if (typeof newValue === 'undefined') {
        if (ko.isObservable(property))
            property = ko.utils.unwrapObservable(property);
        return property;
    } else {
        if (ko.isObservable(property)) {
            property(newValue);
        } else {
            updatePropertyValue(element, entity, newValue, bindingHandlerName, bindingFieldName);

        }
    }
}

function updatePropertyValue(element, viewModel, newValue, bindingHandlerName, bindingFieldName) {
    var reg = new RegExp(bindingHandlerName + "\\s*:\\s*(?:{.*,?\\s*" + bindingFieldName + "\\s*:\\s*)?([^{},\\s]+)"),
        key,
        res = $(element).attr('data-bind').match(reg);
    if (res) {
        key = res[1];
        if (typeof viewModel[key] !== 'undefined') {
            viewModel[key] = newValue;
        }
    }
}
//#region ko.bindingHandlers

ko.bindingHandlers.nullableChecked = {
    init: function (element, valueAccessor, allBindingsAccessor, data, bindingContext) {
        var entity = data, element = element, bindingHandlerName = 'nullableChecked', bindingFieldName = 'value';
        $(element).on('click', function (e) {
            var vm = bindingContext.$root;
            var value = valueAccessor();

            switch (getSetHelper(value)) {
                // unchecked, going indeterminate
                case false:
                    if (vm.status() === 'C')
                        getSetHelper(value, null, element, entity, bindingHandlerName, bindingFieldName);
                    else
                        getSetHelper(value, true, element, entity, bindingHandlerName, bindingFieldName);
                    break;

                // indeterminate, going checked
                case null:
                    getSetHelper(value, true, element, entity, bindingHandlerName, bindingFieldName);
                    break;

                // checked, going unchecked
                default:
                    getSetHelper(value, false, element, entity, bindingHandlerName, bindingFieldName);
            }
        });

    },
    update: function (element, valueAccessor, allBindingsAccessor, data, bindingContext) {
        var vm = bindingContext.$root;
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (vm.status() === 'C' && value == null) {
            element.indeterminate = true;
            value = null;
        }
        else {
            element.indeterminate = false;
        }
        $(element).prop('checked', (value == null ? false : value));
    }
};

ko.bindingHandlers.lookupControl = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var entity = viewModel, element = element, bindingHandlerName = 'lookupControl', bindingFieldName = 'value';
        var editor = $(element), options;

        options = $.extend({}, valueAccessor());
        if (!options.vm) options.vm = bindingContext.$root;
        options.entity = bindingContext.$data;
        options.value = ko.utils.unwrapObservable(options.value);
        options.textChanged = function (event, args) {
            if (getSetHelper(valueAccessor().value) !== args.value)
                getSetHelper(valueAccessor().value, args.value, element, entity, bindingHandlerName, bindingFieldName);
        };

        editor.lookupControl(options);

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).lookupControl("destroy");
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value, current, editor = $(element);
        value = ko.utils.unwrapObservable(valueAccessor().value);

        editor.lookupControl("value", value);

        editor.lookupControl('option', 'disabled', valueAccessor().disabled);
    }
};
//#endregion ko.bindingHandlers
//#endregion lookupControl

//#region popoverWithBind

ko.bindingHandlers.popoverWithBind = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var options = valueAccessor();
        var ctrlName = options.ctrlName + 'filterRange';
        var cssSelectorForPopoverTemplate = ko.utils.unwrapObservable(options.template);
        var popOverTemplate = "<div id='" + ctrlName + "' class='text-left range'>" + $(cssSelectorForPopoverTemplate).html() + "</div>";
        var hasBind = false;
        var isShow = false;
        $(element).igPopover({
            contentTemplate: popOverTemplate,
            headerTemplate: {
                closeButton: true,
                title: options.headerText
            },
            maxWidth: '250px',
            direction: 'auto',
            position: 'start',
            closeOnBlur: false,
            showOn: 'none',
            hidden: function () {
                isShow = false;
                $(element).removeClass('open');
                $(element).removeAttr('tabindex');
                $('#' + ctrlName + ' input').removeAttr('tabindex');
            },
            shown: function () {
                isShow = true;
                $(element).addClass('open');

                var tabIndex = 1;
                $('#' + ctrlName + ' input').each(function (i, item) {
                    $(item).attr('tabindex', tabIndex++);
                });
                $(element).next().attr('tabindex', tabIndex++);

                $('#' + ctrlName + ' input').first().focus();
            }
        });


        $('div.modal-body').scroll(function () {
            $('.filterRange.open').click();
        });


        $(element).click(function () {
            if (!isShow)
                $(element).igPopover('show');
            else {
                $(element).igPopover('hide');
                return;
            }

            if (!hasBind) {
                var _vm = typeof options.vm === 'function' ? options.vm() : options.vm;
                ko.cleanNode(document.getElementById(ctrlName));
                ko.applyBindings(_vm, document.getElementById(ctrlName));
                hasBind = true;
            }
        });

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).igPopover('destroy');
        });

        return { controlsDescendantBindings: true };
    }
};
//#endregion

//#region Converters
function unwrapObservableArray(observableList, vm, manual) {
    var result = [];
    if (!(vm && vm.metadataInfo))
        return result;

    var dataList = getAbsoluteValue(observableList);
    if (!(dataList.length && dataList.length > 0))
        return result;

    var entityName = dataList[0].typeName;
    if (isNullOrEmpty(entityName) || !vm.metadataInfo[entityName])
        return result.concat(dataList);

    //Check if all list is composed by POCO elements 
    var allIsPOCO = true;
    for (var dIndex = 0; dIndex < dataList.length; dIndex++) {
        if (dataList[dIndex].isPOCO !== true || (typeof dataList[dIndex].ChangeState) != 'undefined') {
            allIsPOCO = false;
            break;
        }
    }
    if (allIsPOCO)
        return result.concat(dataList);

    //Convert elements if necessary    
    for (var dIndex = 0; dIndex < dataList.length; dIndex++) {

        if (((dataList[dIndex].ChangeState) != 'undefined') && dataList[dIndex].ChangeState == 'D') //Delete Mark
            continue;

        if (dataList[dIndex].isPOCO === true)
            result.push(dataList[dIndex]);
        else {
            if (manual === true) {
                var structure = '';
                for (var i = 0; i < vm.metadataInfo[entityName].length; i++) {
                    structure += (isNullOrEmpty(structure) ? '' : ', ') + vm.metadataInfo[entityName][i].key + ': getAbsoluteValue(dataList[dIndex].' + vm.metadataInfo[entityName][i].key + ")";
                }

                if (!isNullOrEmpty(structure)) {
                    var row = eval('[{' + structure + '}]');
                    result.push(row[0]);
                }
            }
            else {
                var row = jQuery.extend({}, dataList[dIndex]);
                row.RowDataId = row.RowDataId();
                for (var i = 0; i < vm.metadataInfo[entityName].length; i++) {
                    row[vm.metadataInfo[entityName][i].key] = getAbsoluteValue(row[vm.metadataInfo[entityName][i].key]);
                }
                result.push(row);
            }
        }
    }

    return result;
}

function convertObservableToList(data, vm) {
    return unwrapObservableArray(data, vm, true);
}

function getAbsoluteValue(value) {
    var result = null;

    if (typeof value == 'function')
        result = value();
    else
        result = value;

    //TODO: Remove this line the next version
    if ((typeof getAbsoluteValue.caller === 'function') && getAbsoluteValue.caller.name === 'dataBind' && (typeof result === 'number')) {
        var body = getAbsoluteValue.caller.toString();
        if (body.startsWith("function (commitData, forceCreating) {") && body.contains("if (rows[idx].dataset.id === getAbsoluteValue(")) {
            result = result.toString();
        }
    }

    return result;
}

function setAbsoluteValue(entity, propertyName, value) {
    if (typeof entity[propertyName] === 'function')
        return entity[propertyName](value);
    else
        return entity[propertyName] = value;
}

function formatTimeZone(data) {
    for (var row = 0; row < data.length; row++) {
        for (var col in data[row]) {
            if (data[row][col] instanceof Date && !isNullOrEmpty(data[row][col])) {
                data[row][col] = getUTCDate(data[row][col]);
            }
        }
    }
    return data;
}

function maxArrayValue(dataList, propertyName) {
    var result = 0;

    if (dataList.length && dataList.length > 0 && (typeof dataList[0][propertyName] === 'number')) {
        result = dataList[0][propertyName];
        for (var i = 1; i < dataList.length; i++) {
            if (dataList[i][propertyName] > result) result = dataList[i][propertyName];
        }
    }

    return result;
}

function minArrayValue(dataList, propertyName) {
    var result = 0;

    if (dataList.length && dataList.length > 0 && (typeof dataList[0][propertyName] === 'number')) {
        result = dataList[0][propertyName];
        for (var i = 1; i < dataList.length; i++) {
            if (dataList[i][propertyName] < result) result = dataList[i][propertyName];
        }
    }

    return result;
}

function getCurrentDate() {
    var dNow = new Date();
    var utc = new Date(dNow.getTime() - dNow.getTimezoneOffset() * 60000)
    return utc;
}

function getUTCDate(date) {
    if (isNullOrEmpty(date)) return null;

    if ((typeof date) === 'string')
        date = new Date(date);
    //Alessandro (20/02/2017): This formula fails in "Daylight Saving Time" transitions.
    //var utc = new Date(date.getTime() + date.getTimezoneOffset() * 60000);
    var utc = new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds(), date.getUTCMilliseconds());
    return utc;
}

function copyProperties(objToCopy, arrProperties) {
    var arr = {};
    $.each(arrProperties, function (key, value) { arr[value] = objToCopy[value](); });
    return arr;
}

function daysDiff(di, df) {
    if (df == undefined) {
        df = new Date();
    }
    var ti = di.getTime();
    var tf = df.getTime();

    return parseInt((tf - ti) / (24 * 3600 * 1000));
}

function encode(unencoded) {
    return encodeURIComponent(unencoded).replace(/'/g, "%27").replace(/"/g, "%22");
}
function decode(encoded) {
    return decodeURIComponent(encoded.replace(/\+/g, " "));
}
//#endregion

//#region IO Operations

function saveExcelBlob(fileName, base64Array) {
    var byteString = atob(base64Array);
    var ab = new ArrayBuffer(byteString.length);
    var ia = new Uint8Array(ab);
    for (var i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }
    saveExcelBlobByContent(fileName, ia);
}
function saveExcelBlobByContent(fileName, content) {
    var blob = new Blob([content], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });

    if (window.navigator && window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveOrOpenBlob(blob, fileName);
    } else {
        var a = document.createElement('a');
        a.href = window.URL.createObjectURL(blob);
        a.download = fileName;
        a.style.display = 'none';
        document.body.appendChild(a);
        a.click();
        delete a;
    }
}
function saveURL(url, fileName) {
    var a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    a.style.display = 'none';
    document.body.appendChild(a);
    a.click();
    delete a;
}
//#endregion

//#region getHeightSuggested
function getGridHeightSuggested() {
    var height = 0, availHeight = screen.availHeight;

    if (availHeight < 894)
        height = 650;
    else if (availHeight < 1045)
        height = 730;
    else if (availHeight < 1195)
        height = 860;
    else height = 1050;

    return height.toString();
}
//#endregion

//#region Muda icone de FullScreen ao pressionar "ESC"
$(document).on('keyup', function (evt) {
    var i = $(".toggleFullScreen i");

    if (evt.keyCode == 27) {
        i.removeClass("fa-compress font-red");
        i.addClass("fa-expand");
    }
});
//#endregion Muda icone de FullScreen ao pressionar "ESC"

//#region Macros for UI
function getUIControlValue(selector) {
    if ($(selector).length == 0) {
        console.error('Selector "' + selector + '" not found');
        return null;
    }

    if ($(selector).hasClass('dashboard-stat')) {//dashboard
        return $(selector + '_value').text();
    }
    if ($(selector).data()['igTextEditor']) {//textbox
        return $(selector).igTextEditor('value');
    }
    if ($(selector).data()['igDatePicker']) { //datePicker
        return $(selector).igDatePicker('value');
    }
    if ($(selector).data()['igMaskEditor']) {  //mask
        return $(selector).igMaskEditor('value');
    }
    if ($(selector).data()['igNumericEditor']) {  //igNumericEditor
        return $(selector).igNumericEditor('value');
    }
    if ($(selector).data()['igPercentEditor']) {  //igPercentEditor
        return $(selector).igPercentEditor('value');
    }
    if ($(selector).data()['igCurrencyEditor']) {  //igCurrencyEditor
        return $(selector).igCurrencyEditor('value');
    }
    if ($(selector).attr('type') === 'checkbox') {//checkbox
        return $(selector).prop('checked');
    }
    if ($(selector)[0].tagName === 'TEXTAREA') {//edit
        return $(selector).val();
    }
    if ($(selector).hasClass('text') || $(selector)[0].tagName === "BUTTON") {//label e button
        return $(selector).text();
    }
    if ($(selector)[0].tagName === "IMG") {//img
        return $(selector).attr("src");
    }
    if ($(selector).data('igCombo')) {  //comboBox
        return $(selector).igCombo('value');
    }

    console.error('No value was manipulated for selector [' + selector + '].');
    return null;
}
function setUIControlValue(selector, value) {
    if ($(selector).length == 0) {
        console.error('Selector "' + selector + '" not found');
        return;
    }

    if ($(selector).hasClass('dashboard-stat')) {  //dashboard
        $(selector + '_value').text(value);
        return;
    }
    if ($(selector).data()['igTextEditor']) { //textbox
        $(selector).igTextEditor('value', value);
        return;
    }
    if ($(selector).data()['igDatePicker']) {//datePicker
        $(selector).igDatePicker('value', value);
        return;
    }
    if ($(selector).data()['igMaskEditor']) {//mask
        $(selector).igMaskEditor('value', value);
        return;
    }
    if ($(selector).data()['igNumericEditor']) {//igNumericEditor
        $(selector).igNumericEditor('value', value);
        return;
    }
    if ($(selector).data()['igPercentEditor']) {//igPercentEditor
        $(selector).igPercentEditor('value', value);
        return;
    }
    if ($(selector).data()['igCurrencyEditor']) {//igCurrencyEditor
        $(selector).igCurrencyEditor('value', value);
        return;
    }
    if ($(selector).attr('type') === 'checkbox') {//checkbox
        $(selector).prop('checked', value);
        return;
    }
    if ($(selector)[0].tagName === 'TEXTAREA') {//edit
        $(selector).val(value);
        return;
    }
    if ($(selector).hasClass('text') || $(selector)[0].tagName === "BUTTON") {//label e button
        $(selector).text(value);
        return;
    }
    if ($(selector)[0].tagName === "IMG") {//img
        $(selector).attr("src", value);
        return;
    }
    if ($(selector).data('igCombo')) {  //comboBox
        $(selector).igCombo('value', value);
        return;
    }
    console.error('No value was manipulated for selector [' + selector + '].');
}
function createCombo(selector, items, textKey, valueKey) {
    if ($(selector).length == 0) {
        console.error('Selector "' + selector + '" not found');
        return null;
    }

    var options = {
        dataSource: items
    };

    if (!isNullOrEmpty(textKey))
        options['textKey'] = textKey;
    if (!isNullOrEmpty(valueKey))
        options['valueKey'] = valueKey;

    $(selector).igCombo(options);
}

function $lx(vm, element) {
    return $(vmControlName(vm, element));
}

function vmControlName(vm, element) {
    if ((typeof element === 'string') && element.length > 1 && strLeft(element, 1) === '#' && (vm && !isNullOrEmpty(vm.viewName))) {
        if (element.indexOf('#' + vm.viewName + "_") === -1) {
            element = '#' + vm.viewName + "_" + strRight(element, element.length - 1);
        }
    }
    return element;
}

function setVisible(vm, controlName, isVisible) {
    var control = $lx(vm, '#' + controlName);

    if (!control.length)
        control = $lx(vm, '#div' + controlName);

    if (control.length) {
        if (isVisible) {
            if (control.hasClass('hide'))
                control.removeClass('hide');
        }
        else {
            if (!control.hasClass('hide'))
                control.addClass('hide');
        }
    }
    else
        console.warn("Controle('" + controlName + "') não encontrado, verifique o nome do mesmo.");
}

//#endregion

//#region prototype extensions
Element.prototype.hasClassName = function (a) {
    return new RegExp("(?:^|\\s+)" + a + "(?:\\s+|$)").test(this.className);
};

Element.prototype.addClassName = function (a) {
    if (!this.hasClassName(a)) {
        this.className = [this.className, a].join(" ");
    }
};

Element.prototype.removeClassName = function (b) {
    if (this.hasClassName(b)) {
        var a = this.className;
        this.className = a.replace(new RegExp("(?:^|\\s+)" + b + "(?:\\s+|$)", "g"), " ");
    }
};

Element.prototype.toggleClassName = function (a) {
    this[this.hasClassName(a) ? "removeClassName" : "addClassName"](a);
};
//#endregion

function rangeFilterTypeChanged(filterType, parentSelector, vm) {
    var control = $(parentSelector);
    if (filterType === 'R') {//is range filter
        if (control.children('.filterByRange').hasClass('hide'))
            control.children('.filterByRange').removeClass('hide');
        control.children('.filterByPredefined').addClass('hide');
    } else {
        require(['managers/predefinedFilters'], function (managerPredefined) {
            managerPredefined.load(vm, function (data) {
                var cbo = control.children('.filterByPredefined').children('.cboPredefinedFilters');
                cbo.igCombo('option', 'dataSource', data);
            });
        });
        if (control.children('.filterByPredefined').hasClass('hide'))
            control.children('.filterByPredefined').removeClass('hide');
        control.children('.filterByRange').addClass('hide');
    }
}


//#region Quick search support
function createQuickSearch(servicePath, repoFmtResult, repoFmtSelection) {
    var qsSelector = $("#quickSearchElement");

    if (qsSelector.length > 0) {
        qsSelector.select2({
            placeholder: "Pesquise a informação desejada",
            minimumInputLength: 3,
            ajax: {
                url: servicePath,
                dataType: 'json',
                quietMillis: 250,
                data: function (term, page) { // page is the one-based page number tracked by Select2
                    return {
                        q: encode(term), //search term
                        page: page // page number
                    };
                },
                results: function (data, page) {
                    var more = (page * 10) < data.InlineCount; // whether or not there are more results available

                    //Adjust items for Select2
                    $.each(data.Results, function (idx, item) { item.id = item.$id; item.disabled = false; });

                    var results = data.Results;
                    if (page == 1)
                        results = [{ id: "-1" }].concat(results);

                    // notice we return the value of more so Select2 knows if more results can be loaded
                    return { results: results, more: more };
                }
            },
            formatResult: repoFmtResult, // omitted for brevity, see the source of this page
            formatSelection: repoFmtSelection, // omitted for brevity, see the source of this page
            dropdownCssClass: "bigdrop", // apply css that makes the dropdown taller
            formatInputTooShort: function (input, min) { var n = min - input.length; return "Por favor entre com " + n + " caractere" + (n == 1 ? "" : "s") + " a mais."; },
            formatNoMatches: function () { return "Nenhum resultado encontrado"; },
            formatInputTooLong: function (input, max) { var n = input.length - max; return "Por favor remova " + n + " caractere" + (n == 1 ? "" : "s") + "."; },
            formatSelectionTooBig: function (limit) { return "Você pode apenas selecionar " + limit + " ite" + (limit == 1 ? "m" : "ns") + "."; },
            formatLoadMore: function (pageNumber) { return "Carregando mais resultados..."; },
            formatSearching: function () { return "Pesquisando..."; },
            escapeMarkup: function (m) { return m; } // we do not want to escape markup since we are displaying html in results
        });

        qsSelector.select2("open");
    }
}
//#endregion

//#region class Linx
var Linx = {
    IO: {
        hasBlobSupport: function () {
            return window.File && window.FileReader && window.FileList && window.Blob;
        },
        saveTxt: function (fileName, textString) {
            if (textString == undefined) throw 'Error: the textString is null.'
            var byteString = atob(Linx.Base64.encode(textString));
            var ab = new ArrayBuffer(byteString.length);
            var ia = new Uint8Array(ab);
            for (var i = 0; i < byteString.length; i++) {
                ia[i] = byteString.charCodeAt(i);
            }
            var blob = new Blob([ia], { type: "Plain/Text" });

            if (window.navigator && window.navigator.msSaveOrOpenBlob) {
                window.navigator.msSaveOrOpenBlob(blob, fileName);
            } else {
                var a = document.createElement('a');
                a.href = window.URL.createObjectURL(blob);
                a.download = fileName;
                a.style.display = 'none';
                document.body.appendChild(a);
                a.click();
                delete a;
            }
        },
        readTxt: function (fileObject, callback) {
            if (fileObject == undefined) throw 'Error: the parameter fileObject is null.'
            var reader = new FileReader();
            reader.onload = function (fileResult) {
                if (typeof (callback) == 'function' && fileResult && fileResult.target && fileResult.target.result)
                    callback(fileResult.target.result);
            };
            reader.readAsBinaryString(fileObject);
        }
    },
    Base64: {
        _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
        encode: function (input) {
            var output = "";
            var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
            var i = 0;

            input = Linx.Base64._utf8_encode(input);

            while (i < input.length) {

                chr1 = input.charCodeAt(i++);
                chr2 = input.charCodeAt(i++);
                chr3 = input.charCodeAt(i++);

                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;

                if (isNaN(chr2)) {
                    enc3 = enc4 = 64;
                } else if (isNaN(chr3)) {
                    enc4 = 64;
                }

                output = output + this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) + this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

            }

            return output;
        },
        decode: function (input) {
            var output = "";
            var chr1, chr2, chr3;
            var enc1, enc2, enc3, enc4;
            var i = 0;

            input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

            while (i < input.length) {

                enc1 = this._keyStr.indexOf(input.charAt(i++));
                enc2 = this._keyStr.indexOf(input.charAt(i++));
                enc3 = this._keyStr.indexOf(input.charAt(i++));
                enc4 = this._keyStr.indexOf(input.charAt(i++));

                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;

                output = output + String.fromCharCode(chr1);

                if (enc3 != 64) {
                    output = output + String.fromCharCode(chr2);
                }
                if (enc4 != 64) {
                    output = output + String.fromCharCode(chr3);
                }

            }

            output = Base64._utf8_decode(output);

            return output;

        },
        _utf8_encode: function (string) {
            string = string.replace(/\r\n/g, "\n");
            var utftext = "";

            for (var n = 0; n < string.length; n++) {

                var c = string.charCodeAt(n);

                if (c < 128) {
                    utftext += String.fromCharCode(c);
                }
                else if ((c > 127) && (c < 2048)) {
                    utftext += String.fromCharCode((c >> 6) | 192);
                    utftext += String.fromCharCode((c & 63) | 128);
                }
                else {
                    utftext += String.fromCharCode((c >> 12) | 224);
                    utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                    utftext += String.fromCharCode((c & 63) | 128);
                }

            }

            return utftext;
        },
        _utf8_decode: function (utftext) {
            var string = "";
            var i = 0;
            var c = c1 = c2 = 0;

            while (i < utftext.length) {

                c = utftext.charCodeAt(i);

                if (c < 128) {
                    string += String.fromCharCode(c);
                    i++;
                }
                else if ((c > 191) && (c < 224)) {
                    c2 = utftext.charCodeAt(i + 1);
                    string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                    i += 2;
                }
                else {
                    c2 = utftext.charCodeAt(i + 1);
                    c3 = utftext.charCodeAt(i + 2);
                    string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                    i += 3;
                }

            }

            return string;
        }
    },
    Linx: {
        decode: function (s, recursive) {
            try {
                if (recursive)
                    while (s != encodeURIComponent(s).replace(/'/g, "%27").replace(/"/g, "%22")) {
                        s = encodeURIComponent(s).replace(/'/g, "%27").replace(/"/g, "%22");
                    }
                else
                    s = encodeURIComponent(s).replace(/'/g, "%27").replace(/"/g, "%22");
            } catch (e) { }
            return s;
        },
        encode: function (s, recursive) {
            try {
                s = encodeURIComponent(s).replace(/'/g, "%27").replace(/"/g, "%22");
            } catch (e) { }
            return s;
        }
    }
};
//#endregion class Linx
//#region class ctrl
var ctrl = {
    hasCustomEnable: function (vm, controlName) {
        return !isNull(ctrl.getCustomEnable(vm, controlName));
    },
    getCustomEnable: function (vm, controlName) {
        if (isNull(vm) || isNullOrEmpty(controlName)) throw 'Error: argument vm or controlName is null';
        if (isNull(vm.customStates) || vm.customStates.length === 0)
            return undefined;

        controlName = controlName[0] === '#' ? controlName : '#' + controlName;
        controlName = vmControlName(vm, controlName);

        return vm.customStates[controlName];
    },
    setCustomEnable: function (vm, controlName, enabled, dontNotifyUI) {
        if (isNull(vm) || isNullOrEmpty(controlName)) throw 'Error: argument vm or controlName is null';

        controlName = controlName[0] === '#' ? controlName : '#' + controlName;
        controlName = vmControlName(vm, controlName);

        if (isNull(vm.customStates)) vm.customStates = [];

        if (!vm.customStates.contains(controlName))
            vm.customStates.push(controlName);

        vm.customStates[controlName] = enabled;
        try {
            if (!dontNotifyUI) {
                if (controlName.contains('_btn') && $(controlName).length > 0) {// recreate the bind, if control is button
                    ko.cleanNode($(controlName)[0])
                    ko.applyBindings(vm, $(controlName)[0])
                }

                if (typeof vm.lazyRefreshBinding === 'function') {
                    vm.lazyRefreshBinding();
                }
                else {
                    vm.currentDataItem.notifySubscribers();
                }

            }
        } catch (e) { console.log(e); }
    },
    removeCustomEnable: function (vm, controlName) {
        if (isNull(vm) || isNullOrEmpty(controlName)) throw 'Error: argument vm or controlName is null';

        controlName = controlName[0] === '#' ? controlName : '#' + controlName;
        controlName = vmControlName(vm, controlName);

        if (!isNull(vm.customStates) && vm.customStates.contains(controlName)) {
            vm.customStates.removeItem(controlName);
            try {
                if (typeof vm.lazyRefreshBinding === 'function') {
                    vm.lazyRefreshBinding();
                }
                else {
                    vm.currentDataItem.notifySubscribers();
                }
            } catch (e) { console.log(e); }
        }
    },
    setCustomEnableAll: function (vm, enabled, container) {
        $((container ? container + ' ' : '') + '.form-control').each(function (i, e) {
            ctrl.setCustomEnable(vm, '#' + e.id, enabled, true);
        });
        try {
            if (!dontNotifyUI) {
                if (typeof vm.lazyRefreshBinding === 'function') {
                    vm.lazyRefreshBinding();
                }
                else {
                    vm.currentDataItem.notifySubscribers();
                }
            }
        } catch (e) { console.log(e); }
    }
}
//#endregion class ctrl
function hasObjectWithPropertyValues(ref, propertiesSource) {
    //true =  has values to search, false - no values
    if (isNullOrEmpty(ref) || isNullOrEmpty(propertiesSource)) return false;

    var pSrc = propertiesSource.split(',');

    for (var i = 0; i < pSrc.length; i++) {
        var pS = pSrc[i].toString().trim();
        var v = getAbsoluteValue(ref[pS]);

        if (!isNullOrEmpty(v))
            return true;
    }

    return false;
}
function getObjectWithPropertyValues(ref, propertiesSource, propertiesDest) {
    if (isNullOrEmpty(ref) || isNullOrEmpty(propertiesSource)) return 'null';
    var o = [];

    var pSrc = propertiesSource.split(',');
    var pDst = propertiesDest.split(',');

    if (pSrc.length != pDst.length) return o;

    for (var i = 0; i < pSrc.length; i++) {
        var pS = pSrc[i].toString().trim();
        var pD = pDst[i].toString().trim();
        var v = getAbsoluteValue(ref[pS]);

        o.push(pD + ":" + (isNull(v) ? '' : v).toString());
    }

    return o.join(',');
}


function lxSummarize(originArray, dimensionsArray, measuresArray, vm) {
    var dimDefinition = '';
    var dimFilter = '';
    var result = '';

    if (vm) {
        if (typeof originArray == 'function')
            originArray = unwrapObservableArray(originArray, vm);
        else
            originArray = unwrapObservableArray(function () { return originArray; }, vm);
    }

    if (originArray.length == 0)
        return [];

    //Dimensions
    for (idx in dimensionsArray) {
        var dim = dimensionsArray[idx];
        dimDefinition += (dimDefinition == '' ? '' : ', ') + dim + ': row.' + dim;
        dimFilter += (dimFilter == '' ? '' : ', ') + dim + ': gr.' + dim;
    }

    if (dimDefinition == '')
        return [];


    dimDefinition = '{ ' + dimDefinition + ' }';
    result = dimFilter;
    dimFilter = '{ ' + dimFilter + ' }';

    //Measures
    for (idx in measuresArray) {
        var mesure = measuresArray[idx];
        result += (result == '' ? '' : ', ') + mesure + ': _.sum(details, function(m) { return ' + (mesure == 'Count' ? '1' : 'm.' + mesure) + '; })';
    }

    //Result
    result = '{ ' + result + ', details: details' + ' }';

    var preGroup = _.map(originArray, function (row) { eval('var dimDefinitionDef = ' + dimDefinition + ';'); return dimDefinitionDef; });
    var dimensions = _.uniq(
        preGroup,
        function (item) { return JSON.stringify(item); }
    );

    var resultArray = _.map(
        dimensions
        ,
        function (gr) {
            eval('var dimFilterDef = ' + dimFilter + ';');
            var details = _.filter(originArray, dimFilterDef);
            eval('var resultDef = ' + result + ';');
            return resultDef;
        }
    );

    return _.sortByOrder(resultArray, dimensionsArray);
}

function scrollMainTop() {
    $('#main').scrollTop(0);
}
function parseIsoDatetime(dtstr) {
    var dt = dtstr.split(/[: T-]/).map(parseFloat);
    return new Date(dt[0], dt[1] - 1, dt[2], dt[3] || 0, dt[4] || 0, dt[5] || 0, 0);
}
function parseDate(dtstr) {
    var value = new Date(dtstr);
    if (!isNaN(value)) {
        return value;
    } else {
        return parseIsoDatetime(dtstr);
    }

}


function resizeToolbar() {
    var titlePage = $('h1.title');
    var dataToolbar = $('#screen-meta-links');
    var divSelect2 = ($('div.sidebar-new').width() == null || $('div.sidebar-new').width() == 0 ? 0 : $('div.sidebar-new').width());
    var divRede = ($('div.redebar').width() == null || $('div.redebar').width() == 0 ? 145 : $('div.redebar').width() + 90);
    var width = $(document).width() - dataToolbar.width() - divRede - divSelect2 - 180;
    titlePage.width(width);
};

function linxHelper(status, viewName, rootDataTypeName, helpTags) {

    if (helpTags.contains('MODAprod') || helpTags.contains('Moda')) {
        window.open('https://share.linx.com.br/display/MODAprod/Linx+UX');
    }
    else {
        if (status === 'E')
            window.open('https://share.linx.com.br/dosearchsite.action?cql=siteSearch ~ "title: (' + viewName + ' OR ' + rootDataTypeName + ') AND (Alter OR Inclusão OR Excl)" and space in (' + helpTags + ')');
        else
            window.open('https://share.linx.com.br/dosearchsite.action?cql=siteSearch ~ "title: (' + viewName + ' OR ' + rootDataTypeName + ') AND (Consulta OR Pesquisa)" and space in (' + helpTags + ')');
    }
}

//Task 108824 - TraceGP
function viewDetached(view, viewClosed) {

}

function getLayoutFormPadrao(vm) {
    if (typeof vm.layout() == "object") {
        vm.flattenLayout(ko.observable(vm.flattenObjectByProperty(vm.layout(), 'Name'))());
        return;
    }

    require(['managers/user'],
        function (managerUser) {
            var common = require('common');
            common.showProcess('#main');
            managerUser.getLayoutPadrao(vm.__moduleId__, vm.viewName).then(function (result) {
                if (result !== null) {
                    vm.layout = ko.observable(JSON.parse(result.ConteudoJson));
                    vm.flattenLayout(ko.observable(vm.flattenObjectByProperty(vm.layout(), 'Name'))());
                    vm.currentLayout(result);
                }
                else {
                    vm.layout = vm.layoutDesigner;
                    vm.currentLayout({ Id: 0, NomeLayout: 'Layout Padrão' });
                }
                common.closeProcess('#main');
            });
    })
};

$(document).ready(function () { resizeToolbar(); });
$(window).resize(function () { resizeToolbar(); });