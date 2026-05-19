let cartCount = 0;
let cartTotal = 0;

const cartItems = document.getElementById("cart-items");
const cartTotalElement = document.getElementById("cart-total");

// Gebruik class i.p.v. id
const cartCountElements = document.querySelectorAll(".cart-count");

const clearCartBtn = document.getElementById("clear-cart-btn");
const checkoutBtn = document.getElementById("checkout-btn");

console.log("localStorage cartCount:", localStorage.getItem("cartCount"));

// Helper functie om alle cart-count elementen te updaten
function updateCartCountDisplay() {
    cartCountElements.forEach(el => {
        el.innerText = cartCount;
    });
}

// Add to cart
function addToCart(productName, price) {

    cartCount++;
    cartTotal += price;

    updateCartCountDisplay();

    localStorage.setItem("cartCount", cartCount);
    localStorage.setItem("cartTotal", cartTotal);

    const li = document.createElement("li");

    const itemText = document.createElement("span");
    itemText.innerText = `${productName} - €${price.toFixed(2)}`;

    const removeBtn = document.createElement("button");
    removeBtn.innerText = "Remove";

    removeBtn.addEventListener("click", () => {
        cartCount--;
        cartTotal -= price;

        updateCartCountDisplay();

        cartTotalElement.innerText = `€${cartTotal.toFixed(2)}`;

        localStorage.setItem("cartCount", cartCount);
        localStorage.setItem("cartTotal", cartTotal);

        li.remove();
    });

    li.appendChild(itemText);
    li.appendChild(removeBtn);

    cartItems.appendChild(li);

    cartTotalElement.innerText = `€${cartTotal.toFixed(2)}`;
    updateCartCountDisplay();
}

// Clear cart
clearCartBtn.addEventListener("click", () => {
    cartCount = 0;
    cartTotal = 0;

    updateCartCountDisplay();

    cartTotalElement.innerText = "€0,00";

    cartItems.innerHTML = "";

    localStorage.removeItem("cartCount");
    localStorage.removeItem("cartTotal");
});

// Checkout
checkoutBtn.addEventListener("click", () => {
    window.location.href = "/Checkout";
});

// Open/close cart sidebar
const cartIcon = document.getElementById("cart-icon");
const cartSidebar = document.getElementById("cart-sidebar");
const closeCart = document.getElementById("close-cart");

cartIcon.addEventListener("click", () => {
    localStorage.setItem("cartCount", cartCount);
    localStorage.setItem("cartTotal", cartTotal);

    cartSidebar.classList.toggle("open");
});

closeCart.addEventListener("click", () => {
    cartSidebar.classList.remove("open");
});