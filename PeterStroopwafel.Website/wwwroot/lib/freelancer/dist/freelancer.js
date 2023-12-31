module.exports =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.l = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// identity function for calling harmony imports with the correct context
/******/ 	__webpack_require__.i = function(value) { return value; };

/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};

/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};

/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 1);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports) {

var g;

// This works in non-strict mode
g = (function() {
	return this;
})();

try {
	// This works if eval is allowed (see CSP)
	g = g || Function("return this")() || (1,eval)("this");
} catch(e) {
	// This works if the window reference is available
	if(typeof window === "object")
		g = window;
}

// g can still be undefined, but nothing to do about it...
// We return undefined, instead of nothing here, so it's
// easier to handle this case. if(!global) { ...}

module.exports = g;


/***/ }),
/* 1 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";
/* WEBPACK VAR INJECTION */(function(global) {

Object.defineProperty(exports, "__esModule", {
  value: true
});
/**
 * @method createUrl
 * @param {Function} fn
 * @param {*} [options]
 * @return {String}
 */
const createUrl = (fn, ...options) => {

  if (typeof fn !== 'function') {

    // Ensure the passed parameter is actually a function.
    throw new Error('Freelancer: Passed parameter must be a function.');
  }

  // Map each of the passed options through the JSON stringify process.
  const params = options.map(JSON.stringify);

  // Transform the passed function into an IIFE and then create a blob.
  const blob = new Blob([`(${fn.toString()})(${params})`], { type: 'application/javascript' });

  // Create a URL from the aforementioned blob that handles the worker logic.
  return URL.createObjectURL(blob);
};

/**
 * @method createFallback
 * @param {String} name
 * @return {Object}
 */
const createFallback = name => {

  return class {

    /**
     * @constructor
     * @return {Object}
     */
    constructor() {

      // Raise an error when a worker isn't supported.
      throw new Error(`Freelancer: Unfortunately the ${name} is not supported by the current browser.`);
    }

  };
};

/**
 * @constant WorkerExtend
 * @type {Worker|Object}
 */
const WorkerExtend = global.Worker || createFallback('Worker');

/**
 * @class Freelancer
 * @extends Worker
 */
class Freelancer extends WorkerExtend {

  /**
   * @constructor
   * @param {*} args
   * @return {Worker}
   */
  constructor(...args) {
    return super(createUrl(...args));
  }

}

exports.Freelancer = Freelancer; /**
                                  * @constant SharedWorkerExtend
                                  * @type {SharedWorker|Object}
                                  */

const SharedWorkerExtend = global.SharedWorker || createFallback('SharedWorker');

/**
 * @class SharedFreelancer
 * @extends SharedWorker
 */
class SharedFreelancer extends SharedWorkerExtend {

  /**
   * @constructor
   * @param {*} args
   * @return {SharedWorker}
   */
  constructor(...args) {
    return super(createUrl(...args));
  }

}

exports.SharedFreelancer = SharedFreelancer;
exports.default = Freelancer;
/* WEBPACK VAR INJECTION */}.call(exports, __webpack_require__(0)))

/***/ })
/******/ ]);