var app = require('app');  // Module to control application life.
var BrowserWindow = require('browser-window');  // Module to create native browser window.

require('shelljs/global');


//Para que o preview consiga realizar a rolagem
app.commandLine.appendSwitch('touch-events', 'false');

// Report crashes to our server.
require('crash-reporter').start();

// Keep a global reference of the window object, if you don't, the window will
// be closed automatically when the javascript object is GCed.
var mainWindow = null;

// Quit when all windows are closed.
app.on('window-all-closed', function () {
    if (process.platform != 'darwin') {
        app.quit();
    }
});



// This method will be called when Electron has done everything
// initialization and ready for creating browser windows.
app.on('ready', function () {

    // Create the browser window.
    mainWindow = new BrowserWindow({ width: 1024, height: 600, frame: false });
    mainWindow.maximize();

    // and load the index.html of the app.
    if (which('ionic') && which('git')) {
        mainWindow.loadUrl('file://' + __dirname + '/index.html');
    } else {
        mainWindow.loadUrl('file://' + __dirname + '/noCompatible.html');
    }

    var shortcut = require('global-shortcut');

    shortcut.register('ctrl+s', function () {
        mainWindow.webContents.send('saveFile');
    });

    shortcut.register('ctrl+t', function () {
        mainWindow.webContents.send('checklist');
    });


    // Open the devtools.
    mainWindow.openDevTools();

    // Emitted when the window is closed.
    mainWindow.on('closed', function () {
        // Dereference the window object, usually you would store windows
        // in an array if your app supports multi windows, this is the time
        // when you should delete the corresponding element.
        mainWindow = null;
    });

    mainWindow.on('blur', function () {
        shortcut.unregister('ctrl+s');
        shortcut.unregister('ctrl+t');

    });

    mainWindow.on('focus', function () {
        shortcut.register('ctrl+s', function () {
            mainWindow.webContents.send('saveFile');
        });

        shortcut.register('ctrl+t', function () {
            mainWindow.webContents.send('checklist');
        });
    });
});

//Select directory to export the project
function SelectDirectory(callback) {
    var dialog = require('dialog');
    var options = { title: 'Selecione o diretório', defaultPath: 'C:/', properties: ['openDirectory'] };
    dialog.showOpenDialog(null, options, callback);
}

function SelectFile(callback) {
    var dialog = require('dialog');

    var options = {
        title: 'Selecione o arquivo',
        defaultPath: 'C:/',
        properties: ['openFile'],
        filters: [
            { name: 'Projeto', extensions: ['lxproj'] }
        ]
    };

    dialog.showOpenDialog(null, options, callback);
}

function IsFullScreen() {
    return mainWindow.isFullScreen();
}

function SetFullScreen(flag) {
    mainWindow.setFullScreen(flag);
}

function Close() {
    mainWindow.close();
}

function Minimize() {
    mainWindow.minimize();
}

//Exposing in a global context
global.selectDirectory = SelectDirectory;
global.selectFile = SelectFile;
global.setFullScreen = SetFullScreen;
global.isFullScreen = IsFullScreen;
global.close = Close;
global.minimize = Minimize;
