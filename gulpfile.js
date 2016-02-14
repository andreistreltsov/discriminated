// Copyright(c) 2016 Andrei Streltsov <andrei@astreltsov.com>
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

var gulp = require('gulp');
var runSequence = require('run-sequence');
var msbuild = require('gulp-msbuild');
var exec = require('child_process').exec;
var rm = require('gulp-remove-files');

gulp.task('build-union', function () {
    return gulp.src('./Discriminated/Discriminated.csproj')
		.pipe(msbuild({
		    targets: ['Clean', 'Build'],
		    properties: { configuration: 'Release' },
            stdout: false
		}));
});

gulp.task('build-generator', function () {
    return gulp.src('./Discriminated.Generator/Discriminated.Generator.csproj')
		.pipe(msbuild({
		    targets: ['Clean', 'Build'],
		    properties: { configuration: 'Debug' },
            stdout: false
		}));
});

gulp.task('remove-generated-files', function () {
    return gulp.src('./Discriminated/*.generated.cs')
        .pipe(rm());
});

gulp.task('run-generator', function (cb) {
    return exec('Discriminated.Generator\\bin\\Debug\\Discriminated.Generator.exe Discriminated', { cwd : "." },  function(err, stdout, stderr){
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
});

gulp.task('build', function (callback) {
    runSequence(
      'build-generator',
      'remove-generated-files',
      'run-generator',
      'build-union',
      function (error) {
          if (error) {
              console.log(error.message);
          } else {
              console.log('FINISHED SUCCESSFULLY');
          }
          callback(error);
      });
});
