//#region Extension methods
String.prototype.in = function (arrayItems) {
    return $.inArray(this.toString(), arrayItems) > -1;
};

function isNullOrEmpty(value) {
    return ((typeof value) === 'undefined') || value === null || value === '';
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
//#endregion Pivot Aggregators

//#region MessageBox
var Buttons = {
    OK: 'OK',
    OKCancel: 'OKCancel',
    YesNo: 'YesNo',
    YesNoCancel: 'YesNoCancel'
};

var ButtonResult = {
    OK: 1,
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
    this.buttons = Buttons.OKCancel;
    this.defaultButton = ButtonResult.OK;
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

    var btnOK = $('<button/>').attr({ id: 'msgboxOKButton' }).append('OK').click(function () { btnClick(ButtonResult.OK); });
    var btnCancel = $('<button/>').attr({ id: 'msgboxCancelButton' }).append('Cancel').click(function () { btnClick(ButtonResult.Cancel); });
    var btnYes = $('<button/>').attr({ id: 'msgboxYesButton' }).append('Yes').click(function () { btnClick(ButtonResult.Yes); });
    var btnNo = $('<button/>').attr({ id: 'msgboxNoButton' }).append('No').click(function () { btnClick(ButtonResult.No); });
    btnOK.addClass('btn'); btnCancel.addClass('btn'); btnYes.addClass('btn'); btnNo.addClass('btn');
    if (msg.defaultButton !== null) {
        if (msg.defaultButton === ButtonResult.OK) {
            btnOK.addClass('btn-primary');
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
        case Buttons.OK:
            $divmsgButtons.append(btnOK);
            break;

        case Buttons.OKCancel:
            $divmsgButtons.append(btnOK);
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

    $(this).parent().append($ctrl);

    $ctrl.igDialog({
        headerText: msg.header,
        width: 450,
        imageClass: msg.imageClass,
        state: "closed",
        modal: true,
        draggable: true,
        resizable: true
    });

    $ctrl.igDialog("open");

    return $ctrl;
}

function messageBoxException(exception) {
    /// <summary>Shows a message for a exception with details.</summary>
    /// <param name="exception" type="Error">The exception that occurred.</param>
    if (exception instanceof Error) {
        var msgInfo = new MessageInformation();
        msgInfo.header = 'Error';
        msgInfo.message = exception.message;
        msgInfo.setButtons(Buttons.OK);
        msgInfo.setDefaultButton(ButtonResult.OK);
        msgInfo.imageClass = ImageClass.alert;
        msgInfo.setFunctionReturn(function (br) { alert(br); });
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

//#region Create Predicate Like
function getPredicateLike(fieldToSearch, inputValue) {
    /// <summary>Create a predicate that choose between: startsWith, endsWith or Contains, simulating a "Sql Like"</summary>
    /// <param name="fieldToSearch" type="string">Field Name.</param>
    /// <param name="inputValue" type="string">Value to search in Field.</param>
    /// <returns type="breeze.Predicate"/>
    var predicate = null;

    if (inputValue === null || inputValue === '') {
        return predicate;
    }

    if (inputValue.toString().indexOf('%') < 0) {
        return breeze.Predicate.create(fieldToSearch, 'Equals', inputValue);
    }

    var addPredicate = function (fieldToSearch, operation, findText) {
        var p1 = breeze.Predicate.create(fieldToSearch, operation, findText);
        predicate = predicate === null ? p1 : predicate.and(p1);
    };

    var values = inputValue.split('%');

    for (var i = 0; i < values.length; i++) {
        var findText = values[i];

        if (i === 0 && inputValue[0] !== '%') {
            addPredicate(fieldToSearch, 'StartsWith', findText);
            continue;
        } else if (i === values.length - 1 && inputValue[inputValue.length - 1] !== '%') {
            addPredicate(fieldToSearch, 'EndsWith', findText);
            continue;
        } else {
            addPredicate(fieldToSearch, 'Contains', findText);
        }
    }

    return predicate;
}
//#endregion Create Predicate Like

//#region lookups

///<summary>Class that contains info for a lookup</summary>
function lookupInformation() {
    ///Take
    this.pageSize = 10;
    ///skip
    this.pageSkip = 0;
    ///total records
    this.totalRecords = -1;
    ///Last Predicate
    this.lastPredicate = null;
    ///toal page
    this.totalPages = function () {
        return parseInt(this.totalRecords === -1 ? 0 : parseInt(this.totalRecords / this.pageSize, 10).toFixed(0), 10);
    };
    this.getCurrentDisplay = function () {
        var totalRecords = (this.totalPages() * this.pageSize) + 1;
        var startRecords = parseInt(this.pageSkip * this.pageSize, 10) + 1;
        var endRecords = (this.pageSkip * this.pageSize) + this.pageSize;
        if (endRecords > this.totalRecords) {
            endRecords = this.totalRecords;
        }
        return parseInt(startRecords, 10).toString() + "-" + parseInt(endRecords, 10).toString() + "/" + (this.totalRecords).toString();
    };
}


function createLookUpWindow(dataContext, currentDataItem, title, lookupName, fieldToSearch, internalLookupSearch, lookupInfo, grid, rowIndex, columnIndex) {
    var divLookupWindows = $('#lookupWindow');
    if (divLookupWindows) {
        divLookupWindows.remove();
    }
    divLookupWindows = $('<div />').attr({ id: 'lookupWindow' });

    var tableLookup = $('<table />').attr({ id: 'lookupTable' });
    var divButtons = $('<div />').addClass('right').css({ 'horizontal-align': '0px' });
    var divContainerButtons = $('<div />');
    var btnConfirm = $('<button />').attr({ id: 'lookupConfirm' }).addClass("btn").addClass("blue").addClass("btn-primary").append("Selecionar");
    var btnCancel = $('<button />').attr({ id: 'lookupCancel' }).addClass("btn").append("Cancelar");

    divContainerButtons.append(btnConfirm);
    divContainerButtons.append(btnCancel);
    divButtons.append(divContainerButtons);


    //#region Creation Toolbar
    var Toolbar = $("<ul />").addClass("breadcrumb").addClass("center");
    toolbar_li = $("<li />");
    toolbar_div = $("<div />").addClass("btn-group").addClass("hidden-phone");
    toolbar_btnFirst = $("<button />").addClass("btn").addClass("tooltips").attr({ disabled: true, id: "btFirstLookup", "data-placement": "top", "data-original-title": "Primeiro" }).append("<i class='icon-fast-backward' />");
    toolbar_btnBack = $("<button />").addClass("btn").addClass("tooltips").attr({ disabled: true, id: "btbackLookup", "data-placement": "top", "data-original-title": "Anterior" }).append("<i class='icon-backward' />");
    toolbar_lblCaption = $("<span />").addClass("caption").attr({ id: 'lblLookupDisplayInfo' });
    toolbar_btnCaption = $("<button />").addClass("btn").attr({ disabled: true });
    toolbar_lblCaption.html(lookupInfo.getCurrentDisplay());
    toolbar_btnNext = $("<button />").addClass("btn").addClass("tooltips").attr({ disabled: lookupInfo.pageSkip === lookupInfo.totalPages(), id: "btNextLookup", "data-placement": "top", "data-original-title": "Próximo" }).append("<i class='icon-forward' />");
    toolbar_btnLast = $("<button />").addClass("btn").addClass("tooltips").attr({ disabled: lookupInfo.pageSkip === lookupInfo.totalPages(), id: "btLastLookup", "data-placement": "top", "data-original-title": "Último" }).append("<i class='icon-fast-forward' />");
    toolbar_btnCaption.append(toolbar_lblCaption);
    toolbar_div.append(toolbar_btnFirst).append(toolbar_btnBack).append(toolbar_btnCaption).append(toolbar_btnNext).append(toolbar_btnLast);
    toolbar_li.append(toolbar_div);
    Toolbar.append(toolbar_li);

    var querySucceeded = function (data) {
        tableLookup.igGrid('option', 'dataSource', data.results);
        var currentDisplay = lookupInfo.getCurrentDisplay();
        toolbar_lblCaption.html(currentDisplay);
        toolbar_btnFirst.attr({ disabled: lookupInfo.pageSkip === 0 });
        toolbar_btnBack.attr({ disabled: lookupInfo.pageSkip === 0 });
        toolbar_btnNext.attr({ disabled: lookupInfo.pageSkip === lookupInfo.totalPages() });
        toolbar_btnLast.attr({ disabled: lookupInfo.pageSkip === lookupInfo.totalPages() });
    };
    //clicks
    toolbar_btnFirst.click(function () { lookupInfo = internalLookupSearch(lookupName, fieldToSearch, 'F', querySucceeded, lookupInfo); });
    toolbar_btnBack.click(function () { lookupInfo = internalLookupSearch(lookupName, fieldToSearch, 'B', querySucceeded, lookupInfo); });
    toolbar_btnNext.click(function () { lookupInfo = internalLookupSearch(lookupName, fieldToSearch, 'N', querySucceeded, lookupInfo); });
    toolbar_btnLast.click(function () { lookupInfo = internalLookupSearch(lookupName, fieldToSearch, 'L', querySucceeded, lookupInfo); });
    //#endregion Toolbar

    divLookupWindows.append(Toolbar); //toolbar
    divLookupWindows.append(tableLookup); //datagrid
    divLookupWindows.append($('<br />'));
    divLookupWindows.append(divButtons); //btn´s confirm and cancel

    $(this).parent().append(divLookupWindows);

    try {
        divLookupWindows.igDialog({
            width: "450px",
            headerText: title,
            state: 'closed',
            modal: true,
            draggable: true,
            resizable: true
        });

        btnConfirm.click(function () {
            var index = 0;
            //get selected row
            var selectedRow = $("#lookupTable").igGrid("selectedRow");
            if (selectedRow !== null) {
                index = selectedRow.index;
            }
            //get data source
            var ds = $("#lookupTable").igGrid("dataSourceObject");

            var selectedItem = ds[index];
            //if has selected row, update the object
            if (selectedItem !== null) {
                dataContext['finalize' + lookupName](currentDataItem, selectedItem, '');
            }
            //close
            divLookupWindows.igDialog('close');

            selectGridRow(grid, rowIndex, columnIndex);
        });

        btnCancel.click(function () {
            divLookupWindows.igDialog('close');
            selectGridRow(grid, rowIndex, columnIndex);
        });

    }
    catch (e) {
        messageBoxException(e);
    }
}

function showLookUp(dataContext, currentDataItem, title, lookupName, fieldToSearch, results, internalLookupSearch, lookupInfo, grid, rowIndex, columnIndex) {

    createLookUpWindow(dataContext, currentDataItem, title, lookupName, fieldToSearch, internalLookupSearch, lookupInfo, grid, rowIndex, columnIndex);

    $('#lookupTable').igGrid({
        width: '100%',
        height: '300px',
        autoGenerateColumns: false,
        defaultColumnWidth: 150,
        dataSource: results,
        columns: dataContext.metadataInfo[lookupName],
        features: [
 //           {
 //               name: 'RowSelectors',
 //               enableCheckBoxes: true,
 //               enableRowNumbering: false
 //           },
            {
                name: 'Selection',
                mode: 'row'
                //               ,multipleSelection: true
            }
        ]
    });

    if (results.length === 1) {
        $('#lookupConfirm').click();
        return;
    }

    if ($('#lookupWindow').hasClass('hidden')) {
        $('#lookupWindow').removeClass('hidden');
    }

    $('#lookupWindow').igDialog('open');
}

//#endregion lookups

//#region selectGridCurrentItem
function selectGridCurrentItem(selectItemAction, primaryKey, ui, currentElement, viewSource) {
    if (!ui.manual) {
        var ds = ui.owner.grid.dataSource;
        var vData = ds.dataView()[ui.row.index];
        if (selectItemAction && primaryKey && vData[primaryKey]) {
            selectItemAction(primaryKey, vData[primaryKey], currentElement, viewSource);
        }
    }
}

function getGridDataSource(ui) {
    return ui.owner.grid.dataSource.dataSource();
}

function getGridVirtualItem(ui, index) {
    return ui.owner.grid.dataSource.dataView()[index];
}

//#endregion selectGridCurrentItem

//#region QBE to Predicate
function getPredicateByQBE(reference) {
    /// <summary>Creates a predicate to a query by exambple</summary>
    /// <param name="reference" type="object">entity.</param>
    /// <returns type="breeze.Predicate"/>
    var predicate = null;

    if ((typeof reference === 'undefined') || reference === null || (typeof reference.myProperties === 'undefined') || reference.myProperties === null) {
        return predicate;
    }

    var addPredicate = function (fieldToSearch, operation, findText) {
        var p1 = breeze.Predicate.create(fieldToSearch, operation, findText);
        predicate = predicate === null ? p1 : predicate.and(p1);
    };
    var addPredicate2 = function (pred) {
        predicate = predicate === null ? pred : predicate.and(pred);
    };

    for (var idx in reference.myProperties) {
        var key = reference.myProperties[idx];
        var value = reference[key]();
        if (value !== null) {
            if (typeof value === 'string' && value !== '' && value !== '00000000-0000-0000-0000-000000000000') { //if string
                var p = getPredicateLike(key, value);
                if (p !== null) { addPredicate2(p); }
            } else if (typeof value === 'number' && value > 0) { //if number
                addPredicate(key, 'Equals', value);
            } else if (value instanceof Date && value.getFullYear() > 1900) { //if DateTime
                addPredicate(key, '>=', new Date(value.getFullYear(), value.getMonth(), value.getDate(), 0, 0, 0, 0));
                addPredicate(key, '<=', new Date(value.getFullYear(), value.getMonth(), value.getDate(), 23, 59, 59, 999));
            } else if (typeof value === 'boolean' && value) { //if boolean
                addPredicate(key, 'Equals', value);
            }
        }
    }

    return predicate;
}
//#endregion QBE to Predicate

//#region JExpression
function getJEntityExpression(reference) {
    /// <summary>Create a JExpression with the filled properties of a entity</summary>
    /// <param name="reference" type="object">entity.</param>
    /// <returns type="string"/>
    var jExpression = '';

    if ((typeof reference === 'undefined') || reference === null || (typeof reference.myProperties === 'undefined') || reference.myProperties === null) {
        return jExpression;
    }

    for (var idx in reference.myProperties) {
        var key = reference.myProperties[idx];
        var value = reference[key]();
        if (value !== null) {
            if (typeof value === 'string' && value !== '' && value !== '00000000-0000-0000-0000-000000000000') { //if string
                var operator = (value.indexOf('%') !== -1 ? 'Like' : '==');
                jExpression += (jExpression === '' ? '' : ';') + key + '#' + operator + '#' + reference.serverDataType[key] + value;
            } else if (typeof value === 'number' && value > 0) { //if number
                jExpression += (jExpression === '' ? '' : ';') + key + '#==#' + reference.serverDataType[key] + value.toString();
            } else if (value instanceof Date && value.getFullYear() > 1900) { //if DateTime
                jExpression += (jExpression === '' ? '' : ';') + key + '#>=#' + reference.serverDataType[key] + value.getFullYear().toString() + '-' + (value.getMonth() + 1).toString() + '-' + value.getDate().toString() + ' 00:00:00.000';
                jExpression += (jExpression === '' ? '' : ';') + key + '#<=#' + reference.serverDataType[key] + value.getFullYear().toString() + '-' + (value.getMonth() + 1).toString() + '-' + value.getDate().toString() + ' 23:59:59.999';
            } else if (typeof value === 'boolean' && value) { //if boolean
                jExpression += (jExpression === '' ? '' : ';') + key + '#==#' + reference.serverDataType[key] + value.toString().toLowerCase();
            }
        }
    }
    
    return reference.typeName + "{" + jExpression + "}";
}
//#endregion JExpression

//#region dataDomain
var dataDomain = {
    domains: [],
    parseDomainNames: function (domainsList) {
        var newList = '';
        if (domainsList && domainsList !== '') {
            var values = domainsList.split(',');
            for (var idx in values) {
                if (!dataDomain.domains[values[idx]]) {
                    newList += (newList === '' ? '' : ',') + values[idx];
                }
            }
        }
        return newList;
    },
    registerDomains: function (logger, controllerServiceName, domainsList) {
        domainsList = this.parseDomainNames(domainsList);
        if (domainsList !== '') {
            $.ajax({
                type: 'GET',
                url: controllerServiceName + '/GetDomainsInfo?domainNames=' + domainsList,
                dataType: 'json',
                cache: false,
                error: function (jqXHR, textStatus, errorThrown) {
                    var msg = 'Error getting the following DataDomains: ' + domainsList;
                    logger.logError(msg, errorThrown, 'GET Fail', true);
                },
                success: function (data) {
                    var domainName = '';
                    var idx = 0;
                    for (var i in data) {
                        var values = data[i].split('#');
                        if (values[0] !== domainName) {
                            domainName = values[0];
                            dataDomain.domains[domainName] = [];
                            idx = 0;
                        }
                        dataDomain.domains[domainName][idx] = { id: values[1], name: values[2] };
                        idx++;
                    }
                }
            });
        }
    },
    getItems: function (domainName) {
        var items = dataDomain.domains[domainName];
        return (items ? items : []);
    },
    getName: function (domainName, value) {
        var name = '';
        var domainItems = this.getItems(domainName);
        if (domainItems) {
            for (var i in domainItems) {
                if (domainItems[i].id === value) {
                    name = domainItems[i].name;
                    break;
                }
            }
        }
        return name;
    },
    getId: function (domainName, name) {
        var id = '';
        var domainItems = this.getItems(domainName);
        if (domainItems) {
            for (var i in domainItems) {
                if (domainItems[i].name === name) {
                    id = domainItems[i].id;
                    break;
                }
            }
        }
        return id;
    }
};
//#endregion dataDomain

//#region Authentication Routine
var lxSecurityInfo;

function setSecurityInfo(data) {
    lxSecurityInfo = data;
}

function getCurrentCompany() {
    if (lxSecurityInfo) {
        return lxSecurityInfo[1];
    } else {
        return '';
    }
}

function getAuthorizationToken() {
    if (lxSecurityInfo) {
        return lxSecurityInfo[2];
    } else {
        return '';
    }
}

function getCurrentUser() {
    if (lxSecurityInfo) {
        return lxSecurityInfo[3];
    } else {
        return '';
    }
}

function getAccessGroup() {
    if (lxSecurityInfo) {
        return lxSecurityInfo[4];
    } else {
        return '';
    }
}

function configureAuthenticationSettings() {
    //Adjust User Informations
    $("#user-name-info").html("<strong>" + getUserName() + "</strong>");
    //Add authenticated headers
    var ajaxAdapter = breeze.config.getAdapterInstance("ajax");
    ajaxAdapter.defaultSettings = {
        beforeSend: function (xhr, settings) {
            xhr.setRequestHeader('Application', getApplicationId());
            xhr.setRequestHeader('CurrentCompany', getCurrentCompany());
            xhr.setRequestHeader('AuthorizationToken', getAuthorizationToken());
            xhr.setRequestHeader('CurrentUser', getCurrentUser());
            xhr.setRequestHeader('AccessGroup', getAccessGroup());
        }
    };
}
//#endregion 

//#region Templates
function loadTemplateByFile(file) {
    $.get('templates/' + file + '.html', function (templates) {
        $('body').append('<div style="display:none">' + templates + '<\/div>');
    });
}
//#endregion Templates

//#region Wizard Methods
function wizardStepChange(controlName, navigation, index) {
    if (typeof (controlName) === 'undefined' || controlName.lenght == 0)
        throw new exception('controlName is null or empty.')
    if (controlName[0] !== '#') 
        controlName = '#' + controlName;
    
    var controlInst = $(controlName);
    var total = navigation.find('li').length;
    var current = index + 1;
    // set wizard title
    $('.step-title', controlInst).text('Step ' + (index + 1) + ' of ' + total);
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

//#region CreateViewInfo
function createViewInfo(metadata, dataView, goToIndex) {
    if ($("#selectorList") !== null) {
        $("#selectorList").remove();
    }

    var pKey = 'RowDataId';
    var columns = [{ key: pKey, headerText: pKey, width: '1%', dataType: 'number', hidden: true }];
    columns.push(metadata);
        
    var selector = $('<div/>').attr({ id: 'selectorList' });
    var table = $('<table />').attr({ id: 'selectorTableInfo' });
    selector.append(table);
    $(document).append(selector);
    table.igGrid({
        width: '100%',
        height: '100%',
        primaryKey: pKey,
        autoGenerateColumns: false,
        dataSource: ko.mapping.toJS(dataView),
        columns: columns,
        features: [{
            name: 'Selection',
            mode: 'row',
            rowSelectionChanged: function (evt, ui) {
                var idx = findIndexByKey(dataView(), pKey, ui.rowID)
                if (idx > -1) goToIndex(idx);
            }
        },
       { name: "Sorting", type: "local" },
       { name: "Resizing" },
       { name: "Filtering", type: "local" }]
    });
    var width = parseInt($(window).width() * 0.7, 10);
    var height = parseInt($(window).height() * 0.6, 10);
    selector.igDialog({
        headerText: 'Seletor',
        width: width,
        height: height,
        state: 'opened',
        modal: true,
        draggable: false,
        resizable: true
    });
}
//#endregion CreateViewInfo

//#region Multimedia
function loadMultimidiaUrl(tableName, value) {
    var multimidiaService = getServiceAddress("LinxTcs0101DocMultimidiaTabela");
    var accessgroup = getAccessGroup();
    var url = "assets/img/no-image.png";

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

        url = multimidiaService + "/getMediaByKey?nomeTabela=" + tableName + "&idChave=" + idKey + "&uidChave=" + uidKey + "&uidGrupoAcesso=" + accessgroup;
    }
    return url;
}
//#endregion Multimedia

//#region Lookup Grid

function selectGridRow(grid, rowIndex, columnIndex) {
    if (!grid || (!rowIndex && rowIndex != 0) || (!columnIndex && columnIndex != 0))
        return;

    grid.igGridSelection("selectRow", rowIndex);
    grid.igGridSelection("selectCell", rowIndex, columnIndex);
    grid.igGridUpdating("startEdit", rowIndex, columnIndex);
};

//EditorProviderLookup
$.ig.EditorProviderLookUp = $.ig.EditorProviderLookUp || $.ig.EditorProvider.extend(
    {
        createEditor: function (updating, key, columnSetting, tabIndex) {
            var element, settings = {};
            if (columnSetting) {
                settings = columnSetting.editorOptions || settings;
            }
            settings.change = function () {
                updating._notifyChanged();
            };

            var div = $('<div />').attr({ id: 'divLookUp' });

            var input = $('<input />').attr({ id: "inputLookUp", tabindex: tabIndex }).css({ width: '79%', height: '80%', float: 'left' });
            input.change(function () {
                var dataToolbar = columnSetting.editorOptions.dataToolbar;
                if (dataToolbar.canUndo())
                    this.parentElement.childNodes[1].click();
            });

            var img = $('<i class="icon-search" />').css({ width: '10%', float: 'right', border: 'None', background: 'None' }).attr({ id: 'imgLookUp' });
            img.click(function () {
                var gridName = columnSetting.editorOptions.gridName;
                var grid = $(gridName);
                var lookUpName = columnSetting.editorOptions.lookUpName;
                var selectedRow = grid.igGridSelection("selectedRow");
                var dataSource = grid.igGrid("option", "dataSource");
                var primaryKeys = grid.igGrid("option", "primaryKey");
                var entity = dataSource[selectedRow.index];
                var value = this.parentElement.childNodes[0].value;
                if (!isNullOrEmpty(value) && columnSetting.editorOptions.dataType === 'number') {
                    value = parseInt(value);
                }
                var columnIndex = $(gridName + "_" + key).data("columnIndex");
                entity.executeLookUp(lookUpName, key, value, grid, selectedRow.index, columnIndex);
            });

            div.append(input);
            div.append(img);
            element = div;
            this.editor = element;
            return element;
        },
        getValue: function () {
            return this.editor.find("#inputLookUp")[0].value;
        },
        setValue: function (val) {
            if (this.editor.find("#inputLookUp")[0].value !== val) {
                this.editor.find("#inputLookUp")[0].value = val;
            }
        },
        setSize: function (width, height) {
            this.editor.css({
                width: width - 1,
                height: height - 1
            });

            var marginValue = height > 16 ? parseInt((height - 16) / 2) : 0;
            this.editor.find("#imgLookUp").css("margin", marginValue + "px 0 0 0");

        },
        setFocus: function () {
            this.editor.find("#inputLookUp")[0].focus();
        },
        validator: function () {
            return null;
        },
        destroy: function () {
            this.editor.destroy();
        }
    });
//EditorProviderLookup

//#endregion Lookup Grid