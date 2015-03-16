var gulp = require('gulp'),
    path = require('path'),
	msbuild = require('gulp-msbuild'),
	args = require('yargs').argv,
    assemblyInfo = require('gulp-dotnet-assembly-info');

var build = {};
    build.configuration = 'Release';
    build.number = process.env.BUILD_NUMBER ? process.env.BUILD_NUMBER : 0 ;
    build.version = '1.0.0.' + build.number;

var solution = {};
    solution.directory = '/src/';
    solution.file = path.join(solution.directory, '*.sln');
    solution.packagesDirectory = path.join(solution.directory, 'packages');


var nuget = {};
    nuget.directory = path.join(solution.directory, '.nuget');
    nuget.file = path.join(nuget.directory, 'NuGet.exe'); 


// Return a stream so gulp can determine completion
gulp.task('clean', function() {
    return gulp
        .src('artifacts', { read: false })
        .pipe(clean());
});


gulp.task('configuration', function(){
    process.stdout.write(JSON.stringify(build));
    process.stdout.write(JSON.stringify(solution));
    process.stdout.write(JSON.stringify(nuget));

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