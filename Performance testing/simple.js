import http from 'k6/http';

export default function () {
  http.get('https://nakov-mvc-node-app.herokuapp.com');
}
