// Set the minimum time for the loading animation to be shown
var minLoadingTime = 300;

// Show the loading animation
var loading = document.getElementById("loading");

// Record the time that the page started loading
var startLoadingTime = new Date().getTime();

// Add an event listener to wait for the page to finish loading
window.addEventListener("load", function () {
    hideLoading();
    disappear();
});
function hideLoading() {
    let loading = document.getElementById('loading');
    setTimeout(() => {
        loading.classList.toggle('fade');
    }, 500);
}
function disappear() {
    setTimeout(() => {
        loading.setAttribute("Hidden","");
    }, 1000);
}