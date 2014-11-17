/* jshint camelcase:false */
var gulp = require('gulp');
var pkg = require('./package.json');
var wiredep = require('wiredep').stream;
var plug = require('gulp-load-plugins')();
var env = plug.util.env;
var log = plug.util.log;
var inject = require('gulp-inject');

gulp.task('help', plug.taskListing);

/**
 * @desc Lint the code
 */
gulp.task('jshint', function () {
    log('Linting the JavaScript');

    var sources = [].concat(pkg.paths.js);
    return gulp
        .src(sources)
        .pipe(plug.jshint('./.jshintrc'))
        .pipe(plug.jshint.reporter('jshint-stylish'));
});

/**
 * @desc Watch files and run jshint
 */
gulp.task('spy', function () {
    log('Watching JavaScript files');

    var js = ['gulpfile.js'].concat(pkg.paths.js);

    gulp
        .watch(js, ['jshint'])
        .on('change', logWatch);

    function logWatch(event) {
        log('*** File ' + event.path + ' was ' + event.type + ', running tasks...');
    }
});

gulp.task('wiredep', function () {
  gulp.src('./index.html')
    .pipe(wiredep({
      directory: './bower_components',
      ignorePath: './bower_components/'
    }))
    .pipe(gulp.dest('.'));

  gulp.src('./index.html')
    .pipe(wiredep({
      directory: './bower_components',
      ignorePath: './'
    }))
    .pipe(gulp.dest('.'));
});

gulp.task('index', function () {
  var target = gulp.src('./index.html');
  // It's not necessary to read the files (will speed up things), we're only after their paths:
  var sources = gulp.src(['./app/**/*.js', './content/**/*.css'], {read: false});

  return target.pipe(inject(sources))
    .pipe(gulp.dest('./'));
});
