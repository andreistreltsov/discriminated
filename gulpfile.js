// Copyright(c) 2016 Andrei Streltsov <andrei@astreltsov.com>
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

var gulp = require('gulp');
var runSequence = require('run-sequence');
var msbuild = require('gulp-msbuild');
var exec = require('child_process').exec;
var rm = require('gulp-remove-files');

gulp.task('build-generator', function () {
    return gulp.src('./Discriminated.Generator/Discriminated.Generator.csproj')
		.pipe(msbuild({
		    targets: ['Clean', 'Build'],
		    properties: { configuration: 'Debug' },
            stdout: false
		}));
});

gulp.task('cleanup-gen-files', function () {
    return gulp.src('./Discriminated/*.generated.cs')
        .pipe(rm());
});

gulp.task('generate-code', ['build-generator', 'cleanup-gen-files'], function (cb) {
    return exec('Discriminated.Generator\\bin\\Debug\\Discriminated.Generator.exe Discriminated', { cwd : "." },  function(err, stdout, stderr){
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
});

gulp.task('build', ['generate-code'], function () {
    return gulp.src('./Discriminated/Discriminated.csproj')
		.pipe(msbuild({
		    targets: ['Clean', 'Build'],
		    properties: { configuration: 'Release' },
            stdout: false
		}));
});

gulp.task('prepare-package', ['build'], function () {
    return gulp.src('Discriminated/bin/Release/Discriminated.dll')
        .pipe(gulp.dest('Discriminated/lib'));
});

gulp.task('package', ['prepare-package'], function (cb) {
    return exec('nuget.exe pack Discriminated/Discriminated.csproj.nuspec', { cwd: "." }, function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
});