// Cria a função
function myDemoFunc(name = "User") {
  const msg = `Hello ${name}!`;
  console.log(msg);
  return msg;
}

async function myPromise() {
  const response = await fetch('https://jsonplaceholder.typicode.com/todos/1')
  const json = await response.json();
  console.log(json);
  /*
    {
      "userId": 1,
      "id": 1,
      "title": "delectus aut autem",
      "completed": false
    }
   */
  return json;
}

// Adiciona ela no window
window.myDemoFunc = myDemoFunc;
window.myPromise = myPromise;