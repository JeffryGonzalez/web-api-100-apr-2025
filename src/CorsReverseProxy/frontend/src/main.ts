import './style.css'


document.querySelector<HTMLDivElement>('#app')!.innerHTML = `
  <div>
  <div>
    <label for="values">Enter values (comma separated):</label>
    <input type="text" id="values" placeholder="e.g. 1,2,3" />
    <button id="calculate">Calculate Statistics</button>
  </div>
  </div>
`

const calculateButton = document.querySelector<HTMLButtonElement>('#calculate')!;
const values = document.querySelector<HTMLInputElement>('#values')!;
calculateButton.addEventListener('click', async () => {
  const inputValues = values.value.split(',').map(v => parseFloat(v.trim())).filter(v => !isNaN(v));
  if (inputValues.length === 0) {
    alert('Please enter valid numbers.');
    return;
  }
  
 await fetch('http://localhost:9093/statistics', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({ values: inputValues })
  })
  .then(response => response.json())
  .then(data => {
    const { mean, median, standardDeviation } = data;
  
  alert(`Statistics: Mean ${mean}, Median ${median}, Standard Deviation ${standardDeviation}`);
  });
});
