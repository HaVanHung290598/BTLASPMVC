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

// Đang bị lỗi blur => ko xác nhận thông qua icon được
function editRow(numberRow, isFinish) {
    const ele = document.getElementById(`dm_${numberRow}`);
    const buttonEdit = document.getElementById(`dm_edit_${numberRow}`);
    const buttonFinishEdit = document.getElementById(`dm_finish_edit_${numberRow}`);

    if (isFinish) {
        const inputChange = document.getElementsByClassName(`dm_input_${numberRow}`);
        ele.innerHTML = inputChange[0].value;
        buttonEdit.style.display = 'inline-block';
        buttonFinishEdit.style.display = 'none';
        return;
    }
    const textContent = ele.textContent;
    const input = document.createElement('input');
    input.type = 'text';
    input.value = textContent;
    input.style.padding = "5px 7px";
    input.style.borderRadius = "5px";
    input.style.border = "0.5px solid";
    input.autofocus = true;
    input.className = `dm_input_${numberRow}`;
    ele.innerHTML = null;
    ele.appendChild(input);

    buttonEdit.style.display = 'none';
    buttonFinishEdit.style.display = 'inline-block';
    buttonFinishEdit.style.color = '#000';

    function revestContent() {
        ele.innerHTML = textContent;
        buttonEdit.style.display = 'inline-block';
        buttonFinishEdit.style.display = 'none';
    }
    input.addEventListener('blur', revestContent);
    input.onkeydown = (e) => {
        if (e.keyCode === 13) {
            input.removeEventListener('blur', revestContent);
            buttonEdit.style.display = 'inline-block';
            buttonFinishEdit.style.display = 'none';
            ele.innerHTML = input.value;
        }
    }
}

function showModal() {
    const layerModal = document.getElementById('modal_add_category_layer');
    const modalContent = document.getElementById('modal_add_category_content');
    layerModal.style.display = 'block';
    // modalContent.style.display = 'flex';
    modalContent.style.left = '50%';
    modalContent.style.opacity = '1';
}

function hiddenModal() {
    const layerModal = document.getElementById('modal_add_category_layer');
    const modalContent = document.getElementById('modal_add_category_content');
    layerModal.style.display = 'none';
    modalContent.style.left = '-350px';
    modalContent.style.opacity = '0';
}

function handleAddCategory(e) {
    // const tr = document.createElement('tr');
    // const tdSTT = document.createElement('td');
    // const tdID = document.createElement('td');
    // const tdName = document.createElement('td');
    // tdSTT.textContent = '4';
    // tdID.textContent = 'dm1004';
    // tdName.textContent = e.target.value;
    // tdName.className = 'category_item_name';
    // tr.appendChild(tdSTT);
    // tr.appendChild(tdID);
    // tr.appendChild(tdName);
    //
    // const table = document.getElementById('category_table');
    // debugger
    // table.children[0].appendChild(tr);
}