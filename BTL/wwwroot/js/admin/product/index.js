function showProductDetail(product) {
    const { anh, chatLieu, gia, kichThuoc, kieuDang, maDanhMuc, maSanPham, mauSac, moTa, tenSanPham, thietKe, thuongHieu, danhMuc } = product;
    document.getElementById("ma_san_pham").innerHTML = `Mã sản phẩm: ${maSanPham}`;
    document.getElementById("ma_san_pham_input").value = maSanPham;
    document.getElementById("ten_san_pham").value = tenSanPham;
    document.getElementById("gia_san_pham").value = gia;
    document.getElementById("chat_lieu_san_pham").value = chatLieu;
    document.getElementById("kieu_dang_san_pham").value = kieuDang;
    document.getElementById("thiet_ke_san_pham").value = thietKe;
    document.getElementById("thuong_hieu_san_pham").value = thuongHieu;
    document.getElementById("mau_sac_san_pham").value = mauSac;
    document.getElementById("kich_thuoc_san_pham").value = kichThuoc;
    document.getElementById("danh_muc_san_pham").value = maDanhMuc;
    document.getElementById("product_img").value = anh;
    const img = document.getElementById("img_avatar_nguoi_dung");
    img.src = `/wwwroot/asset/images/products/${anh}`;
}

function clearContentDraw() {
    document.getElementById("ma_san_pham").innerHTML = "";
    document.getElementById("ten_san_pham").value = "";
    document.getElementById("gia_san_pham").value = "";
    document.getElementById("chat_lieu_san_pham").value = "";
    document.getElementById("kieu_dang_san_pham").value = "";
    document.getElementById("thiet_ke_san_pham").value = "";
    document.getElementById("thuong_hieu_san_pham").value = "";
    document.getElementById("mau_sac_san_pham").value = "";
    document.getElementById("kich_thuoc_san_pham").value = "";
    document.getElementById("danh_muc_san_pham").value = "";
    const img = document.getElementById("img_avatar_nguoi_dung");
    img.src = "/wwwroot/asset/images/product-default.jpg";
}

// sau việc handle showDraw sẽ call api lấy dữ liệu chi tiết để vẽ giao diện
function showDraw(id) {
    const form = document.getElementById("draw_form");
    const showDrawContent = () => {
        const draw = document.getElementById('draw_content');
        const drawLayer = document.getElementById('draw_layer');
        draw.style.right = "0";
        drawLayer.style.display = "block";
    }
    if (id !== undefined) {
        form.action = "/Admin/ProductManager/Edit";
        const showContent = (response) => {
            showDrawContent();
            showProductDetail(response.sanPham);
        };
        $.ajax({
            url: "/Admin/ProductManager/Detail",
            data: { id: id },
            dataType: "json",
            type: "GET",
            success: showContent
        });
    } else {
        form.action = "/Admin/ProductManager/Create";
        clearContentDraw();
        showDrawContent();
    }

}

function hiddenDraw() {
    const draw = document.getElementById('draw_content');
    const drawLayer = document.getElementById('draw_layer');
    draw.style.right = "-700px"; // đang fix style draw có width 600px
    drawLayer.style.display = "none";
    // createProductHTML();
}

function handleAvatar(e) {
    const files = e.target.files;
    const img = document.getElementById('img_avatar_nguoi_dung');
    img.src = window.webkitURL.createObjectURL(files[0]);
}

function deleteProduct(id) {
    if (confirm('Bạn có chắc muốn xóa sản phẩm?')) {
        fetch("/Admin/ProductManager/Delete", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({id: id, test: 'testok'}),
        });
        /*$.ajax({
            url: "/Admin/ProductManager/Delete",
            data: { id: id },
            dataType: "json",
            type: "POST",
        });*/
    }
}

function createProductHTML() {
    const productItem = document.createElement('div');
    productItem.className = "product_item";

    const img = document.createElement('img');
    img.src = "/wwwroot/asset/images/products/giay-nam-buoc-day-da-tron-gnta1901-d..jpg";
    img.width = "200px";
    img.height = "270px";
    img.style.width = "200px";
    img.style.height = "270px";
    productItem.appendChild(img);

    const productInfo = document.createElement('div');
    productInfo.className = "product_info";
    productItem.appendChild(productInfo);

    const handleProduct = document.createElement('div');
    handleProduct.className = "handle_product";
    productInfo.appendChild(handleProduct);

    const span1 = document.createElement('span');
    span1.className = "icon";
    span1.onclick = () => showDraw(3);
    handleProduct.appendChild(span1)

    const span2 = document.createElement('span');
    span2.className = "icon";
    handleProduct.appendChild(span2);

    const i1 = document.createElement('i');
    i1.className = "fas fa-edit";
    span1.appendChild(i1);

    const i2 = document.createElement('i');
    i2.className = "fas fa-trash-alt";
    span2.appendChild(i2);

    const productInfoContent = document.createElement('div');
    productInfoContent.className = "product_info_content";
    productInfo.appendChild(productInfoContent);

    const productName = document.createElement('p');
    productName.className = "product_name";
    productName.innerHTML = "Dây lưng da";
    productInfoContent.appendChild(productName);

    const productStar = document.createElement('p');
    productStar.className = "product_star";
    productInfoContent.appendChild(productStar);

    const i3 = document.createElement('i');
    i3.className = "fas fa-star";
    productStar.appendChild(i3.cloneNode());
    productStar.appendChild(i3.cloneNode());
    productStar.appendChild(i3.cloneNode());
    productStar.appendChild(i3.cloneNode());
    productStar.appendChild(i3.cloneNode());

    const productPrice = document.createElement('p');
    productPrice.className = "product_price";
    productPrice.innerHTML = "150.000 VNĐ";
    productInfoContent.appendChild(productPrice);

    document.getElementById('list_product').appendChild(productItem);
}