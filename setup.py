from setuptools import setup
import glob

package_name = 'environment_template'

setup(
    name=package_name,
    version='0.1.0',
    packages=[package_name],
    data_files=[
        ('share/ament_index/resource_index/packages', [f'resource/{package_name}']),
        (f'share/{package_name}', ['package.xml']),
        (f'share/{package_name}/config', ['config/environment.sh']),
        (f'share/{package_name}/config/location', glob.glob('config/location/*.*')),
        (f'share/{package_name}/config/metric', glob.glob('config/metric/*.*')),
        (f'share/{package_name}/config/topological', glob.glob('config/topological/*.*')),
        (f'share/{package_name}/config/world', glob.glob('config/world/*.*')),
    ],
    zip_safe=True,
    maintainer='james',
    maintainer_email='primordia@live.com',
    description='instance of basic template for standardised map referencing across digital twins',
)
