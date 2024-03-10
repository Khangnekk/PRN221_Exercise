// script.js
const addToCartButtons = document.querySelectorAll('.btn-add-to-cart');
const toast = document.createElement('div');
toast.className = 'toast';
toast.textContent = 'Successfully Added to Cart';

addToCartButtons.forEach((button) => {
    button.addEventListener('click', () => {
        document.body.appendChild(toast);
        console.log('Clicked')
        setTimeout(() => {
            toast.style.display = 'block';
        }, 100); 
        setTimeout(() => {
            toast.style.display = 'none';
        }, 550); 
    });
});

function OnClickPageNumberLink(i) {
    console.log("pageNumber = " + i);
    document.getElementById('pageNumber').value = i;
    document.getElementById('form').submit();
}