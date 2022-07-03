var express = require('express');
const http = require('http');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');

var app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));
app.use('/static', express.static(path.join(__dirname, 'public')))

function homePage(req, res) {
  res.sendFile(path.join(__dirname + '/public/index.html'));
}

app.use('/', homePage);

// catch 404 and forward to error handler
app.use(homePage);

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

// Server
const host = process.env.HOST || '0.0.0.0'
const port = process.env.PORT || 3000
const server = http
  .createServer(app)
  .on('close', () => console.log('closed Http Server'))
  .listen(port, host, () => {
    console.log(`listening on ${host}:${port}`)
  });

// shutdown handling
function handleShutdown (thing) {
  console.log('got %s, starting shutdown', thing)
  if (!server.listening) process.exit(0)
  console.log('closing...')
  server.close(err => {
    if (err) {
      console.error(err)
      return process.exit(1)
    }
    console.log('exiting')
    process.exit(0)
  })
}

process.on('SIGINT', handleShutdown);
process.on('SIGTERM', handleShutdown);
process.on('SIGHUP', handleShutdown);
