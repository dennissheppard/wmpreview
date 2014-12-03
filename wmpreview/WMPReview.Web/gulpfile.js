/* jshint camelcase:false */
var gulp = require('gulp');
var pkg = require('./package.json');
var wiredep = require('wiredep').stream;
var plug = require('gulp-load-plugins')();
var env = plug.util.env;
var log = plug.util.log;
var inject = require('gulp-inject');
var angularFilesort = require('gulp-angular-filesort');
var webserver = require('gulp-webserver');
var jshint = require("gulp-jshint");

gulp.task('help', plug.taskListing);

/**
 * @desc Lint the code
 */
gulp.task('lint', function () {
    HintFiles();
});

/**
 * @desc Watch files and run jshint
 */
gulp.task('watch', function() {
  gulp.watch('src/*.js', ['lint']);
});

gulp.task('build', function () {
  InjectDependecies();
});

gulp.task('webserver', function() {
  gulp.src('./')
    .pipe(webserver({
      livereload: true,
      directoryListing: true,
      open: true,
      port: 2323
    }));
});

function InjectDependecies(){
  var target = gulp.src('./index.html');

  var jsSources = gulp.src('./app/**/*.js').pipe(angularFilesort());
  var cssSources = gulp.src('./content/**/*.css', {read: false});

//  inject(gulp.src(path.js.compile, {read:false}), {
//    ignorePath: '/Users/walterroman/Sites/minesweeper-angular',
//    addRootSlash: false
//  });

  return target.pipe(inject(jsSources, {relative:true}))
    .pipe(inject(cssSources, {relative:true}))
    .pipe(wiredep({
      directory: './bower_components/',
      bowerJson: require('./bower.json'),
      ignorePath: '../..' // bower files will be relative to the root
    }))
    .pipe(gulp.dest('./'));
}

function HintFiles(){
  log('Linting the JavaScript');

  return gulp
    .src('./app/**/*.js')
    .pipe(jshint())
    .pipe(jshint.reporter('jshint-stylish'));
}