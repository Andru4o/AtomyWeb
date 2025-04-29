const CACHE_NAME = "atomy-cache-v1";
const urlsToCache = [
    "/",
    "/css/atomy.min.css",
    "/manifest.json"
];

self.addEventListener("install", event => {
    event.waitUntil(
        caches.open(CACHE_NAME).then(async cache => {
            for (const url of urlsToCache) {
                try {
                    await cache.add(url);
                } catch (err) {
                    console.warn(`Failed to cache ${url}:`, err);
                }
            }
        })
    );
});

self.addEventListener("fetch", event => {
    event.respondWith(
        caches.match(event.request).then(response => response || fetch(event.request))
    );
});