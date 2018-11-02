var slideIndex = 1;
var intervalId;
var btnPrev = $('.btn-gallery-prev');
var btnNext = $('.btn-gallery-next');
showSlides(slideIndex);
startInterval();

btnPrev.click(function() {
    clearInterval(intervalId);
    startInterval();
});

btnNext.click(function() {
    clearInterval(intervalId);
    startInterval();
});

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName('slides');

    if (n > slides.length) {
        slideIndex = 1;
    }
    if (n < 1) {
        slideIndex = slides.length;
    }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slides[slideIndex - 1].style.display = "block";
}

function startInterval() {   
    intervalId = setInterval(function() {
        plusSlides(1);
    }, 5000);
}