$(document).ready(function () {
  

    $('.qty-btn').on('click', function () {
        const $cartItem = $(this).closest('.cart-item');
        const cartId = $cartItem.data('cart-id');
        const unitPrice = parseFloat($cartItem.data('unit-price'));
        const $qtySpan = $cartItem.find('.qty-value');
        const $totalSpan = $cartItem.find('.total-value');

        let quantity = parseInt($qtySpan.text());

        if ($(this).hasClass('increase')) {
            quantity++;
        } else if ($(this).hasClass('decrease') && quantity > 1) {
            quantity--;
        }

        $qtySpan.text(quantity);
        const newTotal = (quantity * unitPrice).toFixed(2);
        $totalSpan.text(newTotal);
      

        // Send update to server (optional)
        //$.ajax({
        //    url: '/api/cart/updatequantity',
        //    type: 'POST',
        //    data: {
        //        cartId: cartId,
        //        quantity: quantity
        //    },
        //    success: function (response) {
        //        console.log('Quantity updated');
        //    },
        //    error: function (xhr) {
        //        console.error('Update failed');
        //    }
        //});
    });
});