async function loadStudents() {

    ClearPage();
    const table = document.getElementById('studentsTable');
    table.style = "display: flex;";

    const tbody = document.getElementById('studentBody');
    tbody.innerHTML = '';

    const res = await fetch('/students/all');

    const students = await res.json();

    for (const s of students) {
        const tr = document.createElement('tr');
        tr.innerHTML = `
                    <td>${s.studentId}</td>
                    <td>${s.firstName}</td>
                    <td>${s.lastName}</td>
                    <td>${s.age}</td>`;
        tbody.appendChild(tr);
    }
}

async function loadGroups() {

    ClearPage();
    const table = document.getElementById('groupsTable');
    table.style = "display: flex;";

    const tbody = document.getElementById('groupBody');
    tbody.innerHTML = '';

    const res = await fetch('/groups/all');

    const groups = await res.json();

    for (const g of groups) {
        const tr = document.createElement('tr');
        tr.innerHTML = `
                    <td>${g.groupId}</td>
                    <td>${g.groupName}</td>`;
        tbody.appendChild(tr);
    }
}

async function addStudent() {
    ClearPage();
    let student = {
        firstName: document.getElementById("firstNameStudent").value,
        lastName: document.getElementById("lastNameStudent").value,
        age: Number(document.getElementById("ageStudent").value),
        groupId: Number(document.getElementById("groupId").value)
    }

    const res = await fetch('/students/add', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            firstName: student.firstName,
            lastName: student.lastName,
            age: student.age,
            groupId: student.groupId
        })
    });

    number = await res.json();

    if (res.ok) Console.Log(`${number} SaveOk`);
    else Console.Log("NotOk");
}

async function updateStudent() {

    const id = document.getElementById("idStudentUpdate").value
    ClearPage();
    let student = {
        firstName: document.getElementById("firstNameUpdate").value,
        lastName: document.getElementById("lastNameUpdate").value,
        age: Number(document.getElementById("ageUpdate").value),
        groupId: Number(document.getElementById("groupIdUpdate").value)
    }

    const res = await fetch(`/students/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(student)
    });

    number = await res.json();

    if (res.ok) Console.Log(`${number} SaveOk`);
    else Console.Log("NotOk");
}

function ClearPage() {
    const ts = document.getElementById('studentsTable');
    const tg = document.getElementById('groupsTable');
    ts.style = "display: none;"
    tg.style = "display: none;"
}
