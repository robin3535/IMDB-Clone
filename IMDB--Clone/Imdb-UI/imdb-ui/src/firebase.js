// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyB6UNFeSEOxLbFBz-azXPag4qkYE1mWfzI",
  authDomain: "bootcamp-d5ab1.firebaseapp.com",
  projectId: "bootcamp-d5ab1",
  storageBucket: "bootcamp-d5ab1.appspot.com",
  messagingSenderId: "45537008199",
  appId: "1:45537008199:web:da86c6c65c313e4aa8ef62",
  measurementId: "G-GTSRDHY57G"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

import {getStorage} from 'firebase/storage'
const storage = getStorage(app);
export {storage}