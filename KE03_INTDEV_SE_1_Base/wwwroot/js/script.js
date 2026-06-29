document.addEventListener("DOMContentLoaded", () => {

    const cartIcon = document.getElementById("cart-icon");
    const cartSidebar = document.getElementById("cart-sidebar");
    const closeCart = document.getElementById("close-cart");

    cartIcon.addEventListener("click", () => {
        cartSidebar.classList.toggle("open");
    });

    closeCart.addEventListener("click", () => {
        cartSidebar.classList.remove("open");
    });

});

function changeQty(button, change) {
    const input = button.parentElement.querySelector("input[name='quantity']");
    let value = parseInt(input.value) || 1;

    value += change;

    if (value < 1) value = 1;

    input.value = value;
}