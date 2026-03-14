const doctorSelect = document.getElementById("DoctorID");
const dateInput = document.getElementById("checkupDate");

doctorSelect.addEventListener("change", loadShift);
dateInput.addEventListener("change", loadBusyTimes);

async function loadShift() {
    const doctorId = doctorSelect.value;

    const response = await fetch(`/Checkup/GetDoctorShift?doctorId=${doctorId}`);
    const shift = await response.json();

    if (shift) {
        dateInput.min = formatTime(shift.startTime);
        dateInput.max = formatTime(shift.endTime);
    }
}

async function loadBusyTimes() {
    const doctorId = doctorSelect.value;
    const date = dateInput.value;

    const response = await fetch(`/Checkup/GetBusyTimes?doctorId=${doctorId}&date=${date}`);
    const busy = await response.json();

    busy.forEach(b => {
        if (date.startsWith(b.substring(0, 16))) {
            alert("This hour is already booked!");
            dateInput.value = "";
        }
    });
}

function formatTime(time) {
    return new Date().toISOString().split("T")[0] + "T" + time.substring(0, 5);
}
