// sau việc handle showDraw sẽ call api lấy dữ liệu chi tiết để vẽ giao diện
function showDraw() {
    const draw = document.getElementById('draw_content');
    const drawLayer = document.getElementById('draw_layer');
    draw.style.right = "0";
    drawLayer.style.display = "block";
}

function hiddenDraw() {
    const draw = document.getElementById('draw_content');
    const drawLayer = document.getElementById('draw_layer');
    draw.style.right = "-700px"; // đang fix style draw có width 600px
    drawLayer.style.display = "none";
}

function handleAvatar(e) {
    const files = e.target.files;
    const img = document.getElementById('img_avatar_nguoi_dung');
    img.src = window.webkitURL.createObjectURL(files[0]);
}

function showPopupDelete(id, isShow) {
    document.getElementById(`btn_delete_account_${id}`).style.display = isShow ? "block" : "none";
}