/** @format */

const apiUrl = "https://localhost:7158/api";

async function getAllProjekt() {
  const response = await fetch(`${apiUrl}/projekt`);
  const projekt = await response.json();
  const table = document.getElementById("projektTable");
  table.innerHTML = "";
  projekt.forEach((p) => {
    table.innerHTML += `
      <tr>
        <td>${p.projektnummer}</td>
        <td>${p.namn}</td>
        <td>${p.startdatum.split("T")[0]}</td>
        <td>${p.slutdatum ? p.slutdatum.split("T")[0] : "N/A"}</td> 
        <td>${p.projektansvarig}</td>
        <td>${getStatusText(p.status)}</td>
        <td>${p.totalpris} kr</td>
        <td><button onclick="showProjektDetails(${p.projektnummer})">Visa</button></td>
      </tr>
    `;
  });
}

//ChatGPT - mappa enumvärdena så vi får rätt text för statusarna
function getStatusText(status) {
  switch (status) {
    case "EjPaborjad":
      return "Ej Påbörjad";
    case "Pagaende":
      return "Pågående";
    case "Avslutad":
      return "Avslutad";
  }
}

async function showProjektDetails(projektnummer) {
  const response = await fetch(`${apiUrl}/projekt/${projektnummer}`);
  const projekt = await response.json();

  document.getElementById("projektnummer").value = projekt.projektnummer;
  document.getElementById("namn").value = projekt.namn;
  document.getElementById("startdatum").value = projekt.startdatum.split("T")[0];
  document.getElementById("slutdatum").value = projekt.slutdatum ? projekt.slutdatum.split("T")[0] : "";
  document.getElementById("projektansvarig").value = projekt.projektansvarig;
  document.getElementById("totalpris").value = projekt.totalpris;
  document.getElementById("status").value = projekt.status;
  document.getElementById("antaltimmar").value = projekt.antalTimmar;

  const kundDropdown = document.getElementById("kundDropdown");
  kundDropdown.value = projekt.kund?.kundnummer || projekt.kundnummer;

  const tjanstDropdown = document.getElementById("tjanstDropdown");
  tjanstDropdown.value = projekt.tjanst?.tjanstId || projekt.tjanstId;

  document.getElementById("projektListSection").classList.add("hidden");
  document.getElementById("projektDetailSection").classList.remove("hidden");
}

async function updateProjekt() {
  const projekt = {
    projektnummer: document.getElementById("projektnummer").value,
    namn: document.getElementById("namn").value,
    startdatum: document.getElementById("startdatum").value + "T00:00:00",
    slutdatum: document.getElementById("slutdatum").value ? document.getElementById("slutdatum").value + "T00:00:00" : null,
    projektansvarig: document.getElementById("projektansvarig").value,
    kundnummer: parseInt(document.getElementById("kundDropdown").value),
    tjanstId: parseInt(document.getElementById("tjanstDropdown").value),
    totalpris: parseFloat(document.getElementById("totalpris").value),
    antalTimmar: parseInt(document.getElementById("antaltimmar").value),
    status: getStatusNumericValue(document.getElementById("status").value),
  };

  const response = await fetch(`${apiUrl}/projekt/${projekt.projektnummer}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(projekt),
  });

  if (response.ok) {
    alert("Projekt uppdaterat!");
    showProjektList();
    getAllProjekt();
  } else {
    alert("Ett fel uppstod.");
  }
}

async function createProjekt() {
  const projekt = {
    namn: document.getElementById("newNamn").value,
    startdatum: document.getElementById("newStartdatum").value,
    slutdatum: document.getElementById("newSlutdatum").value || null,
    projektansvarig: document.getElementById("newProjektansvarig").value,
    kundnummer: parseInt(document.getElementById("newKundDropdown").value),
    tjanstid: parseInt(document.getElementById("newTjanstDropdown").value),
    antalTimmar: parseInt(document.getElementById("newAntalTimmar").value),
    status: getStatusNumericValue(document.getElementById("newStatus").value),
  };

  const response = await fetch(`${apiUrl}/projekt`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(projekt),
  });

  if (response.ok) {
    alert("Projekt skapat!");
    showProjektList();
    getAllProjekt();
  } else {
    alert("Ett fel uppstod.");
  }
}

//ChatGPT - servern förväntar sig en siffra för statusvärdena, vi mappar till rätt siffra här
function getStatusNumericValue(statusText) {
  switch (statusText) {
    case "EjPaborjad":
      return 0;
    case "Pagaende":
      return 1;
    case "Avslutad":
      return 2;
  }
}

function showCreateForm() {
  document.getElementById("projektListSection").classList.add("hidden");
  document.getElementById("projektCreateSection").classList.remove("hidden");
  loadKunder();
  loadTjanster();
}

function showProjektList() {
  document.getElementById("projektListSection").classList.remove("hidden");
  document.getElementById("projektDetailSection").classList.add("hidden");
  document.getElementById("projektCreateSection").classList.add("hidden");
}

// chatgpt hämtar kunder från APIet och populerar vår kund-dropdown
async function loadKunder() {
  try {
    const response = await fetch(`${apiUrl}/kund`);
    const kunder = await response.json();

    const table = document.getElementById("kundTable");
    table.innerHTML = "";
    kunder.forEach((p) => {
      table.innerHTML += `
      <tr>
        <td>${p.kundnummer}</td>
        <td>${p.namn}</td>
        <td>${p.telefonnummer}</td>
      </tr>
    `;
    });

    const kundDropdown = document.getElementById("kundDropdown");
    const newKundDropdown = document.getElementById("newKundDropdown");

    [kundDropdown, newKundDropdown].forEach((dropdown) => {
      if (!dropdown) return;
      dropdown.innerHTML = "";
      kunder.forEach((kund) => {
        const option = document.createElement("option");
        option.value = kund.kundnummer;
        option.textContent = `${kund.namn}`;
        dropdown.appendChild(option);
      });
    });
  } catch (error) {
    console.error("Fel vid hämtning av kunder:", error);
  }
}
// chatgpt hämtar tjänster från APIet och populerar vår tjänst-dropdown
async function loadTjanster() {
  try {
    const response = await fetch(`${apiUrl}/tjanst`);
    const tjanster = await response.json();

    const tjanstDropdown = document.getElementById("tjanstDropdown");
    const newTjanstDropdown = document.getElementById("newTjanstDropdown");

    [tjanstDropdown, newTjanstDropdown].forEach((dropdown) => {
      if (!dropdown) return;
      dropdown.innerHTML = "";
      tjanster.forEach((tjanst) => {
        const option = document.createElement("option");
        option.value = tjanst.tjanstId;
        option.textContent = `${tjanst.namn} - ${tjanst.prisPerTimme}:- per timme`;
        dropdown.appendChild(option);
      });
    });
  } catch (error) {
    console.error("Fel vid hämtning av tjänster:", error);
  }
}

getAllProjekt();
loadKunder();
loadTjanster();
