var archiver = require('archiver');
var fse = require('fs-extra');
var md5File = require('md5-file');
var pjson = require('../package.json');

var appName = pjson.appName;
var projectPath = '../';
var outPutPath = projectPath + 'apps/' + appName + '/_deploy';

var zipFileName = 'app-' + appName + '.zip';
var zipFullPath = outPutPath + '/' + zipFileName;

fse.emptyDirSync(outPutPath)

var output = fse.createWriteStream(zipFullPath);
var archive = archiver('zip');

output.on('close', function () {
    console.log('generate deploy zip file "' + zipFullPath + '": ' + archive.pointer() + ' total bytes')

    fse.outputJsonSync(outPutPath + '/info.json', {
        appName: appName,
        name: zipFileName,
        location: zipFullPath.slice(3),
        length: archive.pointer(),
        MD5: md5File(zipFullPath),
        assemblyVersion: "@AssemblyVersion",
        assemblyFileVersion: "@AssemblyFileVersion",
        assemblyBuildDateTime: "@AssemblyBuildDateTime"
    }, {
        spaces: 2
    });
});


archive.on('error', function (err) {
    console.log("*** ERROR ***")
    throw err;
});

archive.pipe(output);


archive.bulk([
	{
	    src: [
           projectPath + 'apps/**',
           '!' + projectPath + 'apps/apps.json',
           '!' + projectPath + 'apps/' + appName + '/_deploy/**']
	}
]);

archive.finalize();