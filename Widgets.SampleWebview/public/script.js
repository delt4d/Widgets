(() => {
    let seconds = 0;
    let intervalId = null;
    let hasStarted = false;

    const timeEl = document.getElementById("current-time");
    const btnStart = document.getElementById("btn-start");
    const btnPause = document.getElementById("btn-pause");
    const btnReset = document.getElementById("btn-reset");

    const formatTime = (sec) => {
        const hours = String(Math.floor(sec / 3600)).padStart(2, "0");
        const minutes = String(Math.floor((sec % 3600) / 60)).padStart(2, "0");
        const seconds = String(sec % 60).padStart(2, "0");
        return `${hours}:${minutes}:${seconds}`;
    };

    const updateDisplay = () => {
        timeEl.innerText = formatTime(seconds);
    };

    const setButtonVisibility = () => {
        const isRunning = intervalId !== null;

        btnStart.classList.toggle("hidden", isRunning);
        btnPause.classList.toggle("hidden", !isRunning);
        btnReset.classList.toggle("hidden", !hasStarted);

        if (!isRunning && hasStarted && seconds > 0) {
            btnStart.textContent = "Resume";
        } else {
            btnStart.textContent = "Start";
        }
    };

    const startTimer = () => {
        if (intervalId) return;
        intervalId = setInterval(() => {
            seconds++;
            updateDisplay();
        }, 1000);
        hasStarted = true;
        setButtonVisibility();
    };

    const pauseTimer = () => {
        clearInterval(intervalId);
        intervalId = null;
        setButtonVisibility();
    };

    const resetTimer = () => {
        pauseTimer();
        seconds = 0;
        hasStarted = false;
        updateDisplay();
        setButtonVisibility();
    };

    btnStart.addEventListener("click", startTimer);
    btnPause.addEventListener("click", pauseTimer);
    btnReset.addEventListener("click", resetTimer);

    updateDisplay();
    setButtonVisibility();
})();
