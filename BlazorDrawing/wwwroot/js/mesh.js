'use strict';

export function drawMesh() {
    const canvas = document.getElementById('canvas');
    const xy = document.getElementById('xy');
    const size = canvas.clientWidth;
    const tickColor = 'gray';
    const meshColor = 'silver';
    const ctx = canvas.getContext('2d');

    ctx.translate(0.5, 0.5) // waiting a line of one pixel
    let shift = 0;

    while (shift <= size) {
        ctx.strokeStyle = shift % 40 == 0 ? tickColor : meshColor;
        ctx.beginPath();
        ctx.moveTo(shift, size)
        ctx.lineTo(size, size - shift);
        shift += 10;
        ctx.stroke();
    }
    // coda
    ctx.beginPath();
    ctx.strokeStyle = meshColor;
    ctx.rect(0, 0, size - 1, size - 1);
    ctx.stroke();

    canvas.addEventListener("mousemove", function (e) {
        let r = canvas.getBoundingClientRect();
        let x = Math.round(e.clientX - r.left);
        let y = Math.round(e.clientY - r.top);
        xy.textContent = `Pointer: (${x}, ${y})`;
    });
}