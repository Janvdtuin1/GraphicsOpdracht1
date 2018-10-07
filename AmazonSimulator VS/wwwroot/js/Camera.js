function CameraEnScene() {

    var camera, scene, renderer;
    var cameraControls;

    camera = new THREE.PerspectiveCamera(70, window.innerWidth / window.innerHeight, 1, 1000);
    cameraControls = new THREE.OrbitControls(camera);
    camera.position.z = 35;
    camera.position.y = 35;
    camera.position.x = 35;
    cameraControls.update();
    scene = new THREE.Scene();
    renderer = new THREE.WebGLRenderer({ antialias: true });
    renderer.setPixelRatio(window.devicePixelRatio);
    renderer.setSize(window.innerWidth, window.innerHeight + 5);
    document.body.appendChild(renderer.domElement);

    window.addEventListener('resize', onWindowResize, false);
    alert("test");
    
};