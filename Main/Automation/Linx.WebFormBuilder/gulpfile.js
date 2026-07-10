var gulp = require('gulp');
var exec = require('child_process').exec;
var inject = require('gulp-inject');
var runSequence = require('run-sequence');

gulp.task('inject', function () {

  var target = gulp.src('./index.html');

  var sources = gulp.src([
  							'./libs/dist/angular/js/angular.min.js',
  					   	'./libs/dist/api-check/js/api-check.js',
                './libs/dist/ace-builds/js/ace.js',
                './libs/dist/ace-builds/js/ext-language_tools.js',
  							'./libs/dist/**/*.js',
  							'./libs/dist/angular-formly-templates-bootstrap/*.js',
  							'./libs/dist/**/*.css',
  							'./app/app.js',
  							'./app/config.js',
  							'./app/config.route.js',
  							'./app/**/*.js',
                './Content/Bootstrap/css/bootstrap.min.css',
                './Content/font-awesome.css',
                './Content/Custom/style.css',
  							'!./libs/dist/jquery/js/jquery.js'
  						], { read: false });

  return target
  			   .pipe(inject(sources, { addRootSlash: false }))
    		   .pipe(gulp.dest('.'));
});

gulp.task('bower', function() {
  exec('bower install', function (err, stdout, stderr) {
	  exec('bower-installer', function (err, stdout, stderr) {
	    gulp.run('inject');
	  });
  });
});

gulp.task('install-dependences', function() {
    exec('npm install', function (err, stdout, stderr) {
        exec('npm install --save-dev gulp-inject', function (err, stdout, stderr) {
            exec('npm install --save-dev run-sequence', function (err, stdout, stderr) {
                exec('npm install --save-dev bower-installer', function (err, stdout, stderr) {
                    gulp.run('bower');
                });
            });
        });
    });
});

gulp.task('default', ['install-dependences']);
