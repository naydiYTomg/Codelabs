window.startError = function () {
    var gamePieces = [];
    var letters = ["C", "O", "D", "E", "/", " \\ ", " A", " B", " S"];
    var draggingPiece = null;
    var offsetX, offsetY;

    function startGame() {
        const fontSize = 40;
        const tempCanvas = document.getElementById('gameCanvas');
        const tempCtx = tempCanvas.getContext('2d');
        tempCtx.font = `${fontSize}px JetBrains Mono`;

        let totalWidth = letters.reduce((sum, letter) =>
            sum + tempCtx.measureText(letter).width, 0);

        let startX = (window.innerWidth - totalWidth) / 2;
        let currentX = startX;

        letters.forEach((letter, index) => {
            const letterWidth = tempCtx.measureText(letter).width;
            gamePieces.push(new component(
                letter,
                currentX,
                -150 - (index * 60),
                fontSize,
                "white",
                letterWidth
            ));
            currentX += letterWidth;
        });
        myGameArea.start();
    }

    var myGameArea = {
        canvas: document.getElementById("gameCanvas"),
        start: function() {
            this.canvas.width = window.innerWidth;
            this.canvas.height = window.innerHeight;
            this.context = this.canvas.getContext("2d");
            this.interval = setInterval(updateGameArea, 20);

            window.addEventListener('resize', () => {
                this.canvas.width = window.innerWidth;
                this.canvas.height = window.innerHeight;
            });

            this.canvas.addEventListener('mousedown', onMouseDown);
            this.canvas.addEventListener('mousemove', onMouseMove);
            this.canvas.addEventListener('mouseup', onMouseUp);
        },
        clear: function() {
            this.context.fillStyle = "black";
            this.context.fillRect(0, 0, this.canvas.width, this.canvas.height);
        }
    }

    function component(text, x, y, fontSize, color, width) {
        this.text = text;
        this.x = x;
        this.y = y;
        this.fontSize = fontSize;
        this.color = color;
        this.width = width;
        this.gravity = 0.8 + (Math.random() * 0.2 - 0.1);
        this.gravitySpeed = 0 + (Math.random() * 0.4 - 0.2);
        this.bounce = 0.6 + (Math.random() * 0.2 - 0.1);
        this.damping = 0.8 + (Math.random() * 0.1 - 0.05);
        this.isDragging = false;

        this.update = function() {
            const ctx = myGameArea.context;
            ctx.font = `${this.fontSize}px 'JetBrains Mono', monospace`;
            ctx.fillStyle = this.color;
            ctx.fillText(this.text, this.x, this.y);
        }

        this.newPos = function() {
            if (!this.isDragging) {
                this.gravitySpeed += this.gravity;
                this.y += this.gravitySpeed;
                this.hitBottom();
            }
        }

        this.hitBottom = function() {
            const bottom = myGameArea.canvas.height - this.fontSize;
            if (this.y > bottom) {
                this.y = bottom;
                this.gravitySpeed = -(this.gravitySpeed * this.bounce);

                if (Math.abs(this.gravitySpeed) < 1) {
                    this.gravitySpeed = 0;
                } else {
                    this.gravitySpeed *= this.damping;
                }
            }
        }

        this.isMouseOver = function(mx, my) {
            return mx >= this.x && mx <= this.x + this.width && my >= this.y - this.fontSize && my <= this.y;
        }
    }

    function updateGameArea() {
        myGameArea.clear();
        gamePieces.forEach(piece => {
            piece.newPos();
            piece.update();
        });
    }

    function onMouseDown(e) {
        const mouseX = e.clientX;
        const mouseY = e.clientY;

        for (let i = 0; i < gamePieces.length; i++) {
            if (gamePieces[i].isMouseOver(mouseX, mouseY)) {
                draggingPiece = gamePieces[i];
                offsetX = mouseX - draggingPiece.x;
                offsetY = mouseY - draggingPiece.y;
                draggingPiece.isDragging = true;
                draggingPiece.gravitySpeed = 0;
                break;
            }
        }
    }

    function onMouseMove(e) {
        if (draggingPiece) {
            const mouseX = e.clientX;
            const mouseY = e.clientY;
            draggingPiece.x = mouseX - offsetX;
            draggingPiece.y = mouseY - offsetY;
        }
    }

    function onMouseUp(e) {
        if (draggingPiece) {
            draggingPiece.isDragging = false;
            draggingPiece.gravitySpeed = 0.5 + (Math.random() * 0.2 - 0.1);
            draggingPiece = null;
        }
    }
    startGame()
}