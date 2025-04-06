
window.startMatrixRain = function (canvasId, options) {
    let canvas = document.getElementById(canvasId);
    if (!canvas) return;
    let ctx = canvas.getContext("2d");
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;
    let fontSize = options.fontSize || 16;
    let columns = Math.floor(canvas.width / fontSize);
    let interval = options.interval || 33;
    let characters = options.characters || "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    let color = options.color || "#0F0";
    let fadeFactor = options.fadeFactor || 0.05;
    let words = options.words || ["CODELABS"];

    let drops = new Array(columns).fill(0).map(() => Math.floor(Math.random() * canvas.height / fontSize));
    let wordDrops = new Array(columns).fill(null);

    function draw() {
        ctx.fillStyle = `rgba(0, 0, 0, ${fadeFactor})`;
        ctx.fillRect(0, 0, canvas.width, canvas.height);
        ctx.fillStyle = color;
        ctx.font = `${fontSize}px monospace`;

        for (let i = 0; i < drops.length; i++) {
            let letter;

            if (wordDrops[i] !== null) {
                let wordDrop = wordDrops[i];
                let letterIndex = drops[i] - wordDrop.start;

                if (letterIndex < wordDrop.word.length) {
                    letter = wordDrop.word.charAt(letterIndex);
                } else {
                    wordDrops[i] = null;
                    letter = characters.charAt(Math.floor(Math.random() * characters.length));
                }
            } else {
                if (Math.random() < 0.005) {
                    let word = words[Math.floor(Math.random() * words.length)];
                    wordDrops[i] = { word: word, start: drops[i] };
                    letter = word.charAt(0);
                } else {
                    letter = characters.charAt(Math.floor(Math.random() * characters.length));
                }
            }

            ctx.fillText(letter, i * fontSize, drops[i] * fontSize);
            drops[i]++;

            if (drops[i] * fontSize > canvas.height && Math.random() > 0.975) {
                drops[i] = 0;
                wordDrops[i] = null;
            }
        }
    }

    setInterval(draw, interval);
};
