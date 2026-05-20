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