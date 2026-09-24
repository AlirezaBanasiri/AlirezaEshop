(() => {
  const slider = document.querySelector("[data-home-slider]");
  if (!slider) return;

  const slides = Array.from(slider.querySelectorAll(".home-slide"));
  const dots = Array.from(slider.querySelectorAll("[data-slider-dot]"));
  let current = 0;
  let timer;

  const show = (index) => {
    current = (index + slides.length) % slides.length;
    slides.forEach((slide, i) => {
      const active = i === current;
      slide.classList.toggle("is-active", active);
      slide.setAttribute("aria-hidden", String(!active));
    });
    dots.forEach((dot, i) => {
      const active = i === current;
      dot.classList.toggle("is-active", active);
      if (active) dot.setAttribute("aria-current", "true");
      else dot.removeAttribute("aria-current");
    });
  };

  const restart = () => {
    window.clearInterval(timer);
    timer = window.setInterval(() => show(current + 1), 5000);
  };

  slider.querySelector("[data-slider-prev]").addEventListener("click", () => {
    show(current - 1);
    restart();
  });
  slider.querySelector("[data-slider-next]").addEventListener("click", () => {
    show(current + 1);
    restart();
  });
  dots.forEach((dot) => dot.addEventListener("click", () => {
    show(Number(dot.dataset.sliderDot));
    restart();
  }));

  slider.addEventListener("mouseenter", () => window.clearInterval(timer));
  slider.addEventListener("mouseleave", restart);
  slider.addEventListener("focusin", () => window.clearInterval(timer));
  slider.addEventListener("focusout", (event) => {
    if (!slider.contains(event.relatedTarget)) restart();
  });
  restart();
})();
