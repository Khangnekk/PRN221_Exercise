// script.js
const addToCartButtons = document.querySelectorAll('.btn-add-to-cart');
const toast = document.createElement('div');
toast.className = 'toast';
toast.textContent = 'Successfully Added to Cart';

addToCartButtons.forEach((button) => {
    button.addEventListener('click', () => {
        console.log(button.value);
        if (button.value == 0) {
            toast.textContent = 'Out of stock';
            toast.style.backgroundColor = 'red';
        }
        document.body.appendChild(toast);
        console.log('Clicked')
        setTimeout(() => {
            toast.style.display = 'block';
        }, 300); 
        setTimeout(() => {
            toast.style.display = 'none';
        }, 1800); 
    });
});

function OnClickPageNumberLink(i) {
    console.log("pageNumber = " + i);
    document.getElementById('pageNumber').value = i;
    document.getElementById('form').submit();
}

