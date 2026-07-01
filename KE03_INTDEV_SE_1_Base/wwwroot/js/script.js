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
//door timo gemaakt
const searchInput = document.getElementById("SearchInputProduct");
if (searchInput) {
    searchInput.addEventListener("keydown", function (event) {
        if (event.key === "Enter") {
            const searchedProduct = searchInput.value;
            if (searchedProduct.trim() === "") {
                window.location.href = "/Index";
                return;
            }
            fetch(`/?handler=ProductReturner&searchedproduct=${encodeURIComponent(searchedProduct)}`)
                .then(response => response.json())
                .then(renderProducts)
                .catch(console.error);
        }
    });
}
//door timo gemaakt
const sortLowToHigh = document.getElementById("sortLowToHigh");
if (sortLowToHigh) {
    sortLowToHigh.addEventListener("click", function () {
        fetch(`/?handler=PLowToHigh`)
            .then(response => response.json())
            .then(renderProducts)
            .catch(console.error);
    });
}
//door timo gemaakt
const sortHighToLow = document.getElementById("sortHighToLow");
if (sortHighToLow) {
    sortHighToLow.addEventListener("click", function () {
        fetch(`/?handler=PHighToLow`)
            .then(response => response.json())
            .then(renderProducts)
            .catch(console.error);
    });
}

const minInput = document.getElementById("SearchLowestPrice");
const maxInput = document.getElementById("SearchHighestPrice");
//door timo gemaakt
function filterPrice() {
    const min = Number(minInput.value) || 0;
    const maxRaw = maxInput.value.trim();
    const errorMsg = document.getElementById("priceError");

    if (maxRaw !== "" && min > Number(maxRaw)) {
        errorMsg.textContent = "Minimum price cannot be greater than maximum price.";
        return;
    } else {
        errorMsg.textContent = "";
    }

    let url = `/Index?handler=PHighToLowSlider&min=${min}`;
    if (maxRaw !== "") {
        url += `&max=${encodeURIComponent(maxRaw)}`;
    }

    fetch(url)
        .then(response => response.json())
        .then(renderProducts)
        .catch(console.error);
}

if (minInput) minInput.addEventListener("input", filterPrice);
if (maxInput) maxInput.addEventListener("input", filterPrice);
//door timo gemaakt
function filterCategory(categoryId) {
    const url = `/Index?handler=ProductReturnerByCategory&categoryId=${categoryId ?? ""}`;

    fetch(url)
        .then(response => response.json())
        .then(renderProducts)
        .catch(console.error);
}

//door timo gemaakt
function renderProducts(products) {

    const container = document.getElementById("ProductContainer");

    container.innerHTML = "";

    products.forEach(product => {

        container.innerHTML += `
            <div class="product">

                <a href="/Index/Details?id=${product.id}">

                    <img src="/images/product_${product.id}.png"
                         alt="${product.name}">

                    <h2>${product.name}</h2>

                </a>

                <p>${product.description}</p>

                <p>€${product.price.toFixed(2)}</p>

                <form method="post" action="/?handler=AddToCart">

                    <input type="hidden"
                           name="productId"
                           value="${product.id}">

                    <input type="hidden"
                           name="name"
                           value="${product.name}">

                    <input type="hidden"
                           name="price"
                           value="${product.price}">

                    <input type="number"
                           name="quantity"
                           value="1"
                           min="1">

                    <button type="submit">
                        Add to cart
                    </button>

                </form>

            </div>
        `;

    });

}