/* GreenHarvest - template scripts (built on GEC base) */
(function () {
  "use strict";

  var STORAGE_KEY = "gec-theme";
  var root = document.documentElement;

  function currentTheme() {
    return root.getAttribute("data-bs-theme") === "dark" ? "dark" : "light";
  }

  function applyTheme(theme) {
    root.setAttribute("data-bs-theme", theme);
    var meta = document.querySelector('meta[name="theme-color"]');
    if (meta) meta.setAttribute("content", theme === "dark" ? "#032526" : "#ffffff");
    try {
      localStorage.setItem(STORAGE_KEY, theme);
    } catch (e) {
      /* storage unavailable - ignore */
    }
  }

  /* ---------- Hero cross-fade + rotating word ---------- */
  function initHero() {
    var slides = Array.prototype.slice.call(document.querySelectorAll(".hero-bg"));
    var dots = Array.prototype.slice.call(document.querySelectorAll(".hero-dots button"));
    var rotate = document.querySelector(".hero-rotate");
    if (slides.length < 2) return;

    var words = rotate ? (rotate.getAttribute("data-words") || "").split("|") : [];
    var i = 0;
    var timer = null;

    function go(n) {
      slides[i].classList.remove("is-active");
      if (dots[i]) dots[i].classList.remove("is-active");
      i = (n + slides.length) % slides.length;
      slides[i].classList.add("is-active");
      if (dots[i]) dots[i].classList.add("is-active");
      if (rotate && words.length) {
        rotate.classList.add("is-out");
        setTimeout(function () {
          rotate.textContent = words[i % words.length];
          rotate.classList.remove("is-out");
        }, 320);
      }
    }
    function next() { go(i + 1); }
    function start() { timer = setInterval(next, 6000); }
    function reset() { clearInterval(timer); start(); }

    dots.forEach(function (dot, idx) {
      dot.addEventListener("click", function () { go(idx); reset(); });
    });
    start();
  }

  /* ---------- Scroll-to-top ---------- */
  function initScrollTop() {
    var btn = document.getElementById("scrollTop");
    if (!btn) return;
    function toggle() {
      if (window.scrollY > 480) btn.classList.add("is-visible");
      else btn.classList.remove("is-visible");
    }
    window.addEventListener("scroll", toggle, { passive: true });
    btn.addEventListener("click", function () {
      window.scrollTo({ top: 0, behavior: "smooth" });
    });
    toggle();
  }

  /* ---------- Lightbox gallery (GLightbox - MIT) ---------- */
  function initLightbox() {
    if (typeof GLightbox === "function") {
      GLightbox({ selector: ".glightbox", loop: true, touchNavigation: true });
    }
  }

  /* ---------- Bootstrap ScrollSpy (header-aware offset) ---------- */
  function initScrollSpy() {
    if (window.bootstrap && bootstrap.ScrollSpy) {
      // eslint-disable-next-line no-new
      new bootstrap.ScrollSpy(document.body, {
        target: "#navMain",
        rootMargin: "0px 0px -55%",
        smoothScroll: true,
      });
    }

    // The footer (#contact) is the last target and is too short to ever cross
    // the ScrollSpy threshold, so it never lights up. When the page is scrolled
    // to the very bottom, force the Contact link active.
    var links = document.querySelectorAll('#navMain .nav-link[href^="#"]');
    var contactLink = document.querySelector('#navMain .nav-link[href="#contact"]');
    if (!links.length || !contactLink) return;

    function atBottomCheck() {
      var atBottom =
        window.innerHeight + window.scrollY >=
        document.documentElement.scrollHeight - 2;
      if (atBottom) {
        links.forEach(function (l) { l.classList.remove("active"); });
        contactLink.classList.add("active");
      }
    }
    window.addEventListener("scroll", atBottomCheck, { passive: true });
    window.addEventListener("resize", atBottomCheck);
  }

  /* ---------- Reveal sections on scroll ---------- */
  function initScrollReveal() {
    if (!("IntersectionObserver" in window)) return;

    var selector =
      "main .section-head, main [class*='col-'], .hero-inner, .vbg-inner, " +
      ".yt-inner, .menu-aside, .menu-scroll, .gallery-grid, .newsletter-form, .nl-contact";
    var nodes = Array.prototype.slice.call(document.querySelectorAll(selector));
    if (!nodes.length) return;

    nodes.forEach(function (el) {
      el.setAttribute("data-reveal", "");
      // Stagger items that share a parent for a gentle cascade.
      var siblings = Array.prototype.filter.call(el.parentNode.children, function (c) {
        return c.hasAttribute && c.hasAttribute("data-reveal");
      });
      var idx = siblings.indexOf(el);
      if (idx > 0) el.style.transitionDelay = Math.min(idx, 6) * 90 + "ms";
    });

    var observer = new IntersectionObserver(
      function (entries) {
        entries.forEach(function (entry) {
          if (entry.isIntersecting) {
            entry.target.classList.add("is-visible");
            observer.unobserve(entry.target);
          }
        });
      },
      { threshold: 0.12, rootMargin: "0px 0px -8% 0px" }
    );

    nodes.forEach(function (el) { observer.observe(el); });
  }

  /* ---------- Close mobile menu after clicking an anchor ---------- */
  function initMobileMenuClose() {
    var collapseEl = document.getElementById("navMain");
    if (!collapseEl || !window.bootstrap) return;
    collapseEl.querySelectorAll('a.nav-link[href^="#"]').forEach(function (link) {
      link.addEventListener("click", function () {
        if (collapseEl.classList.contains("show")) {
          var inst = bootstrap.Collapse.getOrCreateInstance(collapseEl);
          inst.hide();
        }
      });
    });
  }

  // Swap any image that fails to load (e.g. an outdated stock-photo URL) for a
  // branded inline SVG placeholder, so the layout never shows a broken icon.
  function initImageFallback() {
    var PLACEHOLDER =
      "data:image/svg+xml;charset=UTF-8," +
      encodeURIComponent(
        '<svg xmlns="http://www.w3.org/2000/svg" width="800" height="600" viewBox="0 0 800 600">' +
          '<rect width="800" height="600" fill="#106C56"/>' +
          '<path d="M400 250c-55 0-100 30-100 95 0 50 45 90 100 90s100-40 100-90c0-65-45-95-100-95z" fill="#81C29A" opacity="0.55"/>' +
          '<path d="M400 215c-8 40-40 70-95 80 40 18 80 6 95-30 15 36 55 48 95 30-55-10-87-40-95-80z" fill="#F8E3C2"/>' +
          '<text x="400" y="470" font-family="Poppins, Arial, sans-serif" font-size="30" font-weight="700" fill="#F8E3C2" text-anchor="middle">GreenHarvest</text>' +
          "</svg>"
      );

    function fix(img) {
      if (img.dataset.fallbackApplied) return;
      img.dataset.fallbackApplied = "1";
      img.src = PLACEHOLDER;
      img.removeAttribute("srcset");
    }

    // Catch errors that fire before this script runs.
    document.querySelectorAll("img").forEach(function (img) {
      if (img.complete && img.naturalWidth === 0) fix(img);
      else img.addEventListener("error", function () { fix(img); }, { once: true });
    });

    // Catch any future errors (e.g. lazy-loaded images).
    document.addEventListener(
      "error",
      function (e) {
        var t = e.target;
        if (t && t.tagName === "IMG") fix(t);
      },
      true
    );
  }

  document.addEventListener("DOMContentLoaded", function () {
    document.body.classList.add("js-ready");

    // Footer year
    var year = document.getElementById("year");
    if (year) year.textContent = String(new Date().getFullYear());

    // Theme toggle
    var toggle = document.getElementById("themeToggle");
    if (toggle) {
      toggle.addEventListener("click", function () {
        applyTheme(currentTheme() === "dark" ? "light" : "dark");
      });
    }

    initHero();
    initScrollTop();
    initLightbox();
    initScrollSpy();
    initScrollReveal();
    initMobileMenuClose();
    initImageFallback();
  });
})();
