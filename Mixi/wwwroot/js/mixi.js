//$('#addToCart').on('click', '.btn btn-dark', function () {
//    // Lấy giá trị số lượng
//    var quantity = $('#form1').val();

//    // Gửi giá trị số lượng đến Controller
//    $.ajax({
//        url: '/Cart/AddToCart',
//        type: 'POST',
//        data: { id: '@Model.ProductID', quantity: quantity },
//        success: function (result) {
//            // Xử lý kết quả trả về từ Controller (nếu có)
//        },
//        error: function () {
//            // Xử lý lỗi (nếu có)
//        }
//    });
//});
//const addToCartBtn = document.getElementById("addToCart"); // Lấy button có id là "addToCart"

//addToCartBtn.addEventListener("click", () => { // Thêm sự kiện click vào button
//    const productId = addToCartBtn.dataset.id; // Lấy giá trị của thuộc tính "data-id"
//    alert(`Product ID: ${productId}`); // Alert giá trị "ProductID"
//});
//const quantityInput = document.getElementById("quantity-input");
//const decreaseBtn = document.querySelector(".fa-minus");
//const increaseBtn = document.querySelector(".fa-plus");

//decreaseBtn.addEventListener("click", () => {
//    if (quantityInput.value > 0) {
//        quantityInput.value = parseInt(quantityInput.value) - 1;
//    }
//});

//increaseBtn.addEventListener("click", () => {
//    quantityInput.value = parseInt(quantityInput.value) + 1;
//});

//const addToCartBtn = document.getElementById("addToCart");
//addToCartBtn.addEventListener("click", () => {
//    const quantity = quantityInput.value;
//    const productId = addToCartBtn.dataset.id;
//    alert(`Số lượng: ${quantity}`);
//});
//const addToCartBtn = document.getElementById("addToCart")
//addToCartBtn.addEventListener("click", () => {
//    // Lấy giá trị số lượng
//    var quantity = document.getElementById("quantity-input").value;
//    const productId = addToCartBtn.dataset.guid;
//    //alert(quantity);
//    // Gửi giá trị số lượng đến Controller
//    $.ajax({
//        url: '/Cart/AddToCart',
//        type: 'POST',
//        data: { id: productId, quantity: quantity },
//        success: function (result) {
//        },
//        error: function () {
//            // Xử lý lỗi (nếu có)
//        }
//    });
//});