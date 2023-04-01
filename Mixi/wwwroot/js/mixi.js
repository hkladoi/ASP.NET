function decreaseQuantity() {
    let quantityInput = document.getElementById("quantity-input");
    if (quantityInput.value > 1) {
        quantityInput.stepDown();
    }
    updateQuantity();
}

function increaseQuantity() {
    let quantityInput = document.getElementById("quantity-input");
    quantityInput.stepUp();
    updateQuantity();
}

function updateQuantity() {
    let quantityInput = document.getElementById("quantity-input");
    let cartQuantity = parseInt(quantityInput.value);
    let cartViewModel = Html.Raw(Json.Serialize(Model.CartViewModel));
    cartViewModel.Quantity = cartQuantity;
}