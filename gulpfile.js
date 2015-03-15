var gulp = require('gulp'),
	msbuild = require('gulp-msbuild'),
	args = require('yargs').argv,
    assemblyInfo = require('gulp-dotnet-assembly-info');


// Return a stream so gulp can determine completion
gulp.task('clean', function() {
    return gulp
        .src('artifacts', { read: false })
        .pipe(clean());
});


gulp.task('configuration', function(){

});


gulp.task('build', ['configuration'], function() {
    return gulp
        .src('**/*.sln')
        .pipe(msbuild({
            toolsVersion: 12.0,
            targets: ['Clean', 'Build'],
            errorOnFail: true,
            stdout: true,
            verbosity: 'quiet',
            properties: {clp:'ErrorsOnly'}
        }));
});

gulp.task('default', ['build']);

gulp.task('ci', []);