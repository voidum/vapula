var http = require('http');
http.createServer(function (req, resp) {
	resp.writeHead(200, {'Content-Type': 'text/plain'});
	resp.end('Test Node JS\n');
}).listen(51515, "localhost");
console.log('Node server running at http://localhost:51515');