window.addEventListener("DOMContentLoaded", () => {

    let cartCount = parseInt(localStorage.getItem("cartCount")) || 0;
    let cartTotal = parseFloat(localStorage.getItem("cartTotal")) || 0;

    const shippingCost = 4.99;

    const vat = cartTotal * 0.21;

    const finalTotal = cartTotal + shippingCost;

    document.querySelectorAll(".cart-count").forEach(el => {
        el.innerText = cartCount;
    });

    document.getElementById("vat").innerText = `€${vat.toFixed(2)}`;
    document.getElementById("cart-total").innerText = `€${cartTotal.toFixed(2)}`;
    document.getElementById("shippingCost").innerText = `€${shippingCost.toFixed(2)}`;
    document.querySelector(".finalTotal").innerText = `€${finalTotal.toFixed(2)}`;
});


