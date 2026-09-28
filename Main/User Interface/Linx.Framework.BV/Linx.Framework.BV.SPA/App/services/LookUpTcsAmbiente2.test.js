'use strict';

function getUnassignedEnvironmentIds(allEnvironmentIds, assignedEnvironmentIds) {
    var assigned = {};
    (assignedEnvironmentIds || []).forEach(function (id) { assigned[id] = true; });
    return (allEnvironmentIds || []).filter(function (id) { return !assigned[id]; });
}

function matchesLoggedIdLinx(environmentIdLinx, loggedIdLinx) {
    if (!loggedIdLinx) return true;
    return environmentIdLinx === loggedIdLinx;
}

function filterEnvironmentsByLoggedIdLinx(environments, loggedIdLinx) {
    return (environments || []).filter(function (env) {
        return matchesLoggedIdLinx(env.IdLinx, loggedIdLinx);
    });
}

function extractEditedUserId(entitySearch) {
    if (!entitySearch) return 0;
    entitySearch.EntityName = '';
    var expressions = entitySearch.Expressions || [];
    var fieldPos = -1;
    for (var i = 0; i < expressions.length; i++) {
        if (expressions[i].Name === 'Field' && String(expressions[i].Value) === 'IdUsuario') {
            fieldPos = i;
            break;
        }
    }
    if (fieldPos < 0) return 0;
    var userId = parseInt(String(expressions[fieldPos + 2].Value), 10);
    expressions.splice(fieldPos, 3);
    if (expressions[fieldPos] && expressions[fieldPos].Value === '&&') {
        expressions.splice(fieldPos, 1);
    } else if (fieldPos > 0 && expressions[fieldPos - 1] && expressions[fieldPos - 1].Value === '&&') {
        expressions.splice(fieldPos - 1, 1);
    }
    return userId;
}

function buildAlreadyAddedClientFilter(gridRows, currentId) {
    var condition = '';
    (gridRows || []).forEach(function (row) {
        var idAmbiente = row.IdTcsAmbiente;
        if (idAmbiente == null || idAmbiente === 0 || idAmbiente === currentId) return;
        condition = condition + (condition.length > 0 ? ';&&#' : '') + 'IdTcsAmbiente#!=#I' + idAmbiente;
    });
    if (condition.length === 0) return;
    return 'LookUpTcsAmbiente2{' + condition + '}';
}

var failed = 0;
function assertEqual(actual, expected, name) {
    var actualText = JSON.stringify(actual);
    var expectedText = JSON.stringify(expected);
    if (actualText !== expectedText) {
        failed += 1;
        console.error('FAIL ' + name + '\n  expected: ' + expectedText + '\n  actual:   ' + actualText);
    } else {
        console.log('PASS ' + name);
    }
}

assertEqual(
    getUnassignedEnvironmentIds([10, 20, 30, 40], [20, 40]),
    [10, 30],
    'excludes environments already on TCS_USUARIO_ACESSO for the edited user'
);

assertEqual(
    getUnassignedEnvironmentIds([10, 20], []),
    [10, 20],
    'returns every environment when the edited user has none assigned'
);

assertEqual(
    getUnassignedEnvironmentIds([10], [10]),
    [],
    'returns empty when every environment is already on the Ambientes tab'
);

var search = {
    EntityName: 'LookUpTcsAmbiente2',
    Expressions: [
        { Name: 'Field', Value: 'IdUsuario' },
        { Name: 'Operator', Value: '==' },
        { Name: 'Value', Value: '1002310784' }
    ]
};
assertEqual(extractEditedUserId(search), 1002310784, 'reads IdUsuario from the edited user entity search');
assertEqual(search.EntityName, '', 'clears lookup entity name so TCS_AMBIENTE can be queried');
assertEqual(search.Expressions, [], 'removes IdUsuario so it is not applied to TCS_AMBIENTE');

assertEqual(extractEditedUserId({ Expressions: [] }), 0, 'treats missing IdUsuario as a new user with nothing assigned');

assertEqual(
    buildAlreadyAddedClientFilter([{ IdTcsAmbiente: 10 }, { IdTcsAmbiente: 20 }, { IdTcsAmbiente: 0 }], 0),
    'LookUpTcsAmbiente2{IdTcsAmbiente#!=#I10;&&#IdTcsAmbiente#!=#I20}',
    'client lookup hides environments already present on the Ambientes grid'
);

assertEqual(
    buildAlreadyAddedClientFilter([{ IdTcsAmbiente: 10 }, { IdTcsAmbiente: 20 }], 20),
    'LookUpTcsAmbiente2{IdTcsAmbiente#!=#I10}',
    'keeps the environment of the row being edited selectable'
);

assertEqual(matchesLoggedIdLinx(4, 4), true, 'keeps environments of the logged user IdLinx');
assertEqual(matchesLoggedIdLinx(1, 4), false, 'drops environments from another IdLinx');
assertEqual(
    filterEnvironmentsByLoggedIdLinx(
        [{ IdTcsAmbiente: 1053, IdLinx: 1 }, { IdTcsAmbiente: 1062, IdLinx: 4 }, { IdTcsAmbiente: 3078, IdLinx: 4 }],
        4
    ),
    [{ IdTcsAmbiente: 1062, IdLinx: 4 }, { IdTcsAmbiente: 3078, IdLinx: 4 }],
    'list lookup only returns ambientes whose IdLinx matches the logged user'
);

if (failed > 0) {
    console.error(failed + ' test(s) failed');
    process.exit(1);
}

console.log('All LookUpTcsAmbiente2 helper tests passed');
