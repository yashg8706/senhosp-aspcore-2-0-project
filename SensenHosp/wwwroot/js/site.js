// Write your JavaScript code.

window.onload = function () {
    let donationAmount = 10;
    const amountInput = document.getElementById('donationAmount');
    amountInput.onblur = function () {
        donationAmount = Number(amountInput.value);
        console.log(donationAmount);
    }
    paypal.Buttons({
        /*createOrder: function (data, actions) {
            return actions.order.create({
                Prefer: "return=representation",
                "Content-Type": "Application/json",
                intent: "CAPTURE",
                purchase_units: [{
                    amount: {
                        value: donationAmount
                    }
                }],
                application_context: {
                    brand_name: "Sensen Humber Hospital",
                    shipping_preference: "NO_SHIPPING",
                    user_action: "PAY_NOW"
                }
            });
        }, */
        createOrder: function () {
            return fetch('/Donations/CreateOrder', {
                method: 'post',
                headers: {
                    'content-type': 'application/json'
                }
            }).then(function (res) {
                return res.json();
                }).then(function (data) {
                    return(data.headers[7].value[0]);
            });
        },
        onApprove: function (data) {
            return fetch('/Donations/GetOrder', {
                headers: {
                    'content-type': 'application/json'
                },
                body: JSON.stringify({
                    orderID: data
                })
            }).then(function (res) {
                return res.json();
            }).then(function (details) {
                alert('Transaction funds captured from ' + details.payer_given_name);
            });
    }).render('#paypal-button-container');
}