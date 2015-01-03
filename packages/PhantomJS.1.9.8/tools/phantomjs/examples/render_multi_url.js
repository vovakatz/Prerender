// Render Multiple URLs to file

var RenderUrlsToFile, arrayOfUrls, system;

system = require("system");

/*
Render given urls
@param array of URLs to render
@param callbackPerUrl Function called after finishing each URL, including the last URL
@param callbackFinal Function called after finishing everything
*/
RenderUrlsToFile = function(urls, callbackPerUrl, callbackFinal) {
    var getFilename, next, page, retrieve, urlIndex, webpage;
    urlIndex = 0;
    webpage = require("webpage");
    page = null;
    getFilename = function() {
        return "rendermulti-" + urlIndex + ".png";
    };
    next = function(status, url, file) {
        page.close();
        callbackPerUrl(status, url, file);
        return retrieve();
    };
    retrieve = function() {
        var url;
        if (urls.length > 0) {
            url = urls.shift();
            urlIndex++;
            page = webpage.create();
            page.viewportSize = {
                width: 1200,
                height: 600
            };
            page.settings.userAgent = "Phantom.js bot";
            return page.open("http://" + url, function(status) {
                var file;
                file = getFilename();
                if (status === "success") {
                    return window.setTimeout((function() {
                        page.render(file);
                        return next(status, url, file);
                    }), 2000);
                } else {
                    return next(status, url, file);
                }
            });
        } else {
            return callbackFinal();
        }
    };
    return retrieve();
};

arrayOfUrls = null;

if (system.args.length > 1) {
    arrayOfUrls = Array.prototype.slice.call(system.args, 1);
} else {
    console.log("Usage: phantomjs render_multi_url.js [domain.name1, domain.name2, ...]");
    arrayOfUrls = ["www.townandcountrytoyota.com", "www.townandcountrytoyota.com/find-vehicle#?sortby=highestprice&inventorytype=new&startpage=1", "www.townandcountrytoyota.com/find-vehicle#?sortby=highestprice&inventorytype=new&makes=toyota&startpage=1","www.townandcountrytoyota.com/regional-inventory", "www.townandcountrytoyota.com/find-vehicle#?startpage=1&inventorytype=new&sortby=highestprice&makes=scion", "www.townandcountrytoyota.com/specials/manufacturer-specials","www.townandcountrytoyota.com/models-and-incentives", "www.townandcountrytoyota.com/warranty/toyotacare", "www.townandcountrytoyota.com/warranty/lifetime-warranty","www.townandcountrytoyota.com/finance?section=sellwizard", "www.townandcountrytoyota.com/find-vehicle#?sortby=highestprice&inventorytype=cpo&startpage=1", "www.townandcountrytoyota.com/find-vehicle#?sortby=highestprice&inventorytype=cpo&startpage=1","www.townandcountrytoyota.com/certification-program", "www.townandcountrytoyota.com/finance?section=sellwizard", "www.townandcountrytoyota.com/carfinder","www.townandcountrytoyota.com/find-vehicle#?sortby=highestprice&inventorytype=used&startpage=1"];
}

RenderUrlsToFile(arrayOfUrls, (function(status, url, file) {
    if (status !== "success") {
        return console.log("Unable to render '" + url + "'");
    } else {
        return console.log("Rendered '" + url + "' at '" + file + "'");
    }
}), function() {
    return phantom.exit();
});
